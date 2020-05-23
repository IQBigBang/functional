using System;
using System.Collections.Generic;
using System.Linq;
using Functional.ast;
using Functional.debug;
using Functional.parser.patterns;
using Functional.types;

namespace Functional.engines
{
    public class TypeChecker : AstVisitor
    {
        /// <summary>
        /// Contains types of global functions (not modified by FunctionNode)
        /// </summary>
        private Dictionary<string, FunctionType> GlobalFunctions;

        /// <summary>
        /// Contains current function symbols (set by FunctionNode)
        /// </summary>
        private Dictionary<string, Ty> CurrentFunctionSymbols;

        /// <summary>
        /// Contains all the types hidden under a NamedType
        /// </summary>
        private TypeTable TypeTable;

        public void CheckAll((List<FunctionNode>, List<TypeDefinitionNode>) definitions, ref TypeTable typeTable)
        {
            GlobalFunctions = new Dictionary<string, FunctionType>();
            CurrentFunctionSymbols = new Dictionary<string, Ty>();
            TypeTable = typeTable;

            foreach (var def in definitions.Item2)
            {
                TypeTable.Insert(def.Name, def.ActualType);

                // If it is an AndType, add a constructor to the function list
                if (def.ActualType.Is<AndType>())
                {
                    var namedType = new Ty(def.Name, def.ActualType, ref TypeTable);
                    var andType = def.ActualType.As<AndType>();

                    var ConstructorType = andType.Members.Concat(new Ty[] { namedType }).ToArray();
                    GlobalFunctions.Add(def.Name, new FunctionType(ConstructorType));
                } 
                // If it is an OrType, add variant constructors to the function list
                else if (def.ActualType.Is<OrType>())
                {
                    var namedType = new Ty(def.Name, def.ActualType, ref typeTable);
                    var orType = def.ActualType.As<OrType>();

                    Array.ForEach(orType.Variants, (variant) =>
                    {
                        var VariantConstructorType =
                            new FunctionType(new Ty[2] { variant.Item2, namedType });
                        GlobalFunctions.Add(variant.Item1, VariantConstructorType);
                    });
                }
            }

            foreach (var f in definitions.Item1)
            {
                GlobalFunctions.Add(f.Name, f.Predicate);
            }

            definitions.Item1.ForEach((f) => Visit(f));
        }

        public override void VisitBinOp(BinOpNode node)
        {
            Visit(node.Lhs);
            Visit(node.Rhs);
            if (node.Op == "+" || node.Op == "-" || node.Op == "*")
            {
                if (node.Lhs.NodeType.Type.Is<IntType>()
                    && node.Rhs.NodeType.Type.Is<IntType>())
                {
                    node.NodeType = new Ty("Int", ref TypeTable);
                }
                else if (node.Op == "+" 
                         && node.Lhs.NodeType.Type.Is<StringType>()
                         && node.Rhs.NodeType.Type.Is<StringType>()) 
                {
                    node.NodeType = new Ty("String", ref TypeTable);
                } else
                {
                    node.ReportError("Invalid binary operation for `{0}`. Left hand side is {1} but right hand side is {2}",
                        node.Op, node.Lhs.NodeType, node.Rhs.NodeType);
                }
            }
            else if (node.Op == ":")
            {
                if (node.Rhs.NodeType.Type.Is<ListType>()
                    && node.Rhs.NodeType.Type.As<ListType>().ListElementsType == node.Lhs.NodeType)
                {
                    node.NodeType = new Ty(new ListType(node.Lhs.NodeType), ref TypeTable);
                } 
                else
                {
                    node.ReportError("Invalid binary operation for `{0}`. Left hand side is {1} but right hand side is {2}",
                        node.Op, node.Lhs.NodeType, node.Rhs.NodeType);
                }
            }
            else
            {
                node.ReportError("Invalid binary operation `{0}`", node.Op);
            }
        }

        public override void VisitCall(CallNode node)
        {
            Visit(node.Callee);
            for (int i = 0; i < node.Args.Length; i++) Visit(node.Args[i]);

            if (!node.Callee.NodeType.Type.Is<FunctionType>())
                node.ReportError("Cannot call a non-function");

            var ftype = node.Callee.NodeType.Type.As<FunctionType>();

            if (ftype.InnerTypes.Length - 1 != node.Args.Length)
                node.ReportError("Invalid argument count for a call: expected {0} arguments but got {1}",
                    ftype.InnerTypes.Length - 1, node.Args.Length);

            for (int i = 0; i < node.Args.Length; i++)
            {
                if (node.Args[i].NodeType != ftype.InnerTypes[i])
                    node.ReportError("Invalid argument passed to a function: expected {0} but got {1}",
                        ftype.InnerTypes[i],
                        node.Args[i].NodeType);
            }

            // the last 'InnerType' of a function if its return type
            node.NodeType = ftype.InnerTypes.Last();
        }

        public override void VisitConstant(ConstantNode node)
        {
            return; // Type already supplied
        }

        public override void VisitFunction(FunctionNode node)
        {

            // 'main' is a special case, it should always take zero arguments
            if (node.Name == "main")
            {
                if (!(node.Predicate.InnerTypes.Length == 1
                    && node.Predicate.InnerTypes[0].Name == "Nil"))
                    node.ReportError("Main function should have a signature `main :: Nil`");
            }

            if (node.IsExternal) return;

            foreach (var overload in node.Overloads)
            {
                // check argument count
                if (overload.Item1.Length != node.Predicate.InnerTypes.Length - 1)
                    overload.Item2.ReportError("The number of arguments does not match the predicate: expected {0} but got {1}",
                        node.Predicate.InnerTypes.Length - 1, overload.Item1.Length);

                // check the types of the patterns
                for (int i = 0; i < overload.Item1.Length; i++)
                {
                    if (!overload.Item1[i].MatchesType(node.Predicate.InnerTypes[i]))
                        overload.Item2.ReportError("This pattern cannot be used for type {0}", node.Predicate.InnerTypes[i]);

                    overload.Item1[i].SetType(node.Predicate.InnerTypes[i]);
                }

                // find the binded arguments
                CurrentFunctionSymbols.Clear();

                foreach (var pat in overload.Item1)
                {
                    pat.GetBindingsTypes(ref CurrentFunctionSymbols);
                }

                // resolve the 'where' clause
                if (!(overload.Item3 is null))
                {
                    Visit(overload.Item3);
                }

                Visit(overload.Item2);
                if (overload.Item2.NodeType != node.Predicate.InnerTypes.Last())
                    overload.Item2.ReportError("The function body returned an unexpected type: expected {0} but got {1}", 
                        node.Predicate.InnerTypes.Last(), overload.Item2.NodeType);

                node.NodeType = new Ty(node.Predicate, ref TypeTable);
            }
        }

        public override void VisitListNode(ListNode node)
        {
            Array.ForEach(node.Elements, Visit);

            // First handle empty lists
            if (node.Elements.Length == 0)
            {
                if (node.TypeHint is Ty typeHint)
                {
                    node.NodeType = new Ty(new ListType(typeHint), ref TypeTable);
                }
                else node.ReportError("Empty lists require a type hint otherwise their type can't be inferred");
                return;
            }
            if (node.TypeHint is Ty) node.ReportWarning("Type hints are ignored for non-empty lists");

            // Check if all list elements are of the same type
            Ty expectedType = node.Elements[0].NodeType;
            foreach (var el in node.Elements)
            {
                if (el.NodeType != expectedType)
                    node.ReportError("Invalid type inside a list. Expected {0} but got {1}", expectedType, el.NodeType);
            }

            node.NodeType = new Ty(new ListType(expectedType), ref TypeTable);
        }

        public override void VisitTypeDefinition(TypeDefinitionNode node)
        {
            return;
        }

        public override void VisitVar(VarNode node)
        {
            if (node.Name == "main")
                node.ReportError("Main should never be called or used in another function");

            if (GlobalFunctions.ContainsKey(node.Name))
                node.NodeType = new Ty(GlobalFunctions[node.Name], ref TypeTable);
            else if (CurrentFunctionSymbols.ContainsKey(node.Name))
                node.NodeType = CurrentFunctionSymbols[node.Name];
            else
                node.ReportError("Undefined variable `{0}`", node.Name);
        }

        public override void VisitWhereClause(WhereClauseNode node)
        {
            foreach (var (patt, value) in node.Bindings)
            {
                Visit(value);

                // First ensure the pattern and the value agree
                if (!patt.MatchesType(value.NodeType))
                    value.ReportError("This pattern cannot be used for type {0}", 
                        value.NodeType);

                patt.SetType(value.NodeType);

                // Then add bindings
                patt.GetBindingsTypes(ref CurrentFunctionSymbols);
            }
        }
    }
}
