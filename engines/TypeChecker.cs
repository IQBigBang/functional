using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Functional.ast;
using Functional.debug;
using Functional.parser.patterns;
using Functional.types;

namespace Functional.engines
{
    public class TypeChecker : AstVisitor
    {
        /// <summary>
        /// Contains all global functions (not modified by FunctionNode)
        /// in format (name, function-type)
        /// </summary>
        public List<(string, FunctionType)> GlobalFunctions { get; private set; }

        /// <summary>
        /// Contains current function symbols (set by FunctionNode)
        /// </summary>
        private Dictionary<string, Ty> CurrentFunctionSymbols;

        /// <summary>
        /// Contains all the types hidden under a NamedType
        /// </summary>
        private TypeTable TypeTable;

        /// <summary>
        /// A list of all global generic functions
        /// </summary>
        private List<FunctionNode> GenericFunctions;

        /// <summary>
        /// A list of all functions created by monomorphization
        /// </summary>
        private List<FunctionNode> NewMonomorphizedFunctions;

        /// <summary>
        /// Checks all nodes.
        /// This might introduce new monomorphized generic functions,
        /// so this function returns a new list of all functions
        /// </summary>
        /// <param name="definitions">Definitions.</param>
        /// <param name="typeTable">Type table.</param>
        public List<FunctionNode> CheckAll((List<FunctionNode>, List<FunctionNode>, List<TypeDefinitionNode>) definitions, ref TypeTable typeTable)
        {
            GlobalFunctions = new List<(string, FunctionType)>();
            NewMonomorphizedFunctions = new List<FunctionNode>();
            CurrentFunctionSymbols = new Dictionary<string, Ty>();
            TypeTable = typeTable;

            foreach (var def in definitions.Item3)
            {
                VisitTypeDefinition(def);
            }

            GenericFunctions = definitions.Item2;
            
            foreach (var f in definitions.Item1)
            {
                GlobalFunctions.Add((f.Name, f.Predicate));
            }

            definitions.Item1.ForEach((f) => Visit(f));

            NewMonomorphizedFunctions.AddRange(definitions.Item1);
            return NewMonomorphizedFunctions;
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
            for (int i = 0; i < node.Args.Length; i++) Visit(node.Args[i]);

            // If you use a direct call in form `f x y`, supply the type hint
            if (node.Callee is VarNode varnode)
            {
                if (varnode.UserSuppliedTypeHint)
                    ErrorReporter.Warning("Unnecessary type hint - the type will be inferred");
                varnode.TypeHint = new Ty(new FunctionType(
                    node.Args.Select((x) => x.NodeType).ToArray()
                ), ref TypeTable);
            }

            Visit(node.Callee);

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
            // If it is an AndType, add a constructor to the function list
            if (node.ActualType.Is<AndType>())
            {
                var namedType = new Ty(node.Name, node.ActualType, ref TypeTable, node.FileAndLine);
                var andType = node.ActualType.As<AndType>();

                var ConstructorType = andType.Members.Concat(new Ty[] { namedType }).ToArray();
                GlobalFunctions.Add((node.Name, new FunctionType(ConstructorType)));
            }
            // If it is an OrType, add variant constructors to the function list
            else if (node.ActualType.Is<OrType>())
            {
                var namedType = new Ty(node.Name, node.ActualType, ref TypeTable, node.FileAndLine);
                var orType = node.ActualType.As<OrType>();

                Array.ForEach(orType.Variants, (variant) =>
                {
                    var VariantConstructorType =
                        new FunctionType(new Ty[2] { variant.Item2, namedType });
                    GlobalFunctions.Add((variant.Item1, VariantConstructorType));
                });
            }
        }

        public override void VisitVar(VarNode node)
        {
            if (node.Name == "main")
                node.ReportError("Main should never be called or used in another function");
            
            if (CurrentFunctionSymbols.ContainsKey(node.Name))
            {
                // Print the warning only if the type-hint is user-supplied, not automatically by the type-checker
                if (!(node.TypeHint is null) && node.UserSuppliedTypeHint)
                    node.ReportWarning("Type hints are ignored for local symbols");
                node.NodeType = CurrentFunctionSymbols[node.Name];
                return;
            }

            // convert hints with just one type (like itoa :: Int) to function-type
            FunctionType hint;
            if (node.TypeHint is null) hint = null;
            else if (!node.TypeHint.Type.Is<FunctionType>())
                hint = new FunctionType(new Ty[] { node.TypeHint });
            else
                hint = node.TypeHint.Type.As<FunctionType>();

            var ViableFunctions =
                GlobalFunctions.Where((x) => x.Item1 == node.Name);
            if (ViableFunctions.Any())
            {
                if (node.TypeHint is null)
                    node.ReportError("The function `{0}` requires a type-hint", node.Name);

                var MatchedFunctions = ViableFunctions.Where((func) =>
                {
                    // The type hint is expected to be without the return type (just arguments)
                    if (func.Item2.InnerTypes.Length != hint.InnerTypes.Length + 1)
                        return false;
                    var okay = true;
                    for (int i = 0; i < hint.InnerTypes.Length; i++)
                        okay &= func.Item2.InnerTypes[i] == hint.InnerTypes[i];
                    return okay;
                }).ToList();

                if (MatchedFunctions.Count > 1)
                    node.ReportError("Multiple alternatives for function {0} of type {1}", node.Name, hint);

                if (MatchedFunctions.Count == 1)
                {
                    // If there is only one function left, we found the right one
                    node.NodeType = new Ty(MatchedFunctions[0].Item2, ref TypeTable);
                    node.Name = MatchedFunctions[0].Item2.GetNamedFunctionMangledName(node.Name);
                    return;
                }
            }

            if (node.TypeHint is null)
                node.ReportError("The variable `{0}` requires a type-hint", node.Name);
                
            // If there are no alternatives, look in generic functions
            var generic = TryMonomorphizeGenericFunction(node.Name, hint, node.FileAndLine);
            if (generic is null)
                node.ReportError("Undefined variable `{0}`", node.Name);

            node.NodeType = new Ty(generic.Predicate, ref TypeTable);
            node.Name = generic.GetMangledName();
            return;
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

        /// <summary>
        /// Tries to find and monomorphize a generic function
        /// </summary>
        /// <param name="name">The name of the function</param>
        /// <param name="type">The function type except for the return type</param>
        /// <returns>If a valid generic function was found, returns the monomorphized version. Otherwise returns null</returns>
        public FunctionNode TryMonomorphizeGenericFunction(string name, FunctionType type, string FileAndLine)
        {
            // For each function, try if it is a valid candidate
            Console.WriteLine("Asking for generic function named {0}", name);
            var ValidFunctions = new List<(FunctionNode, Dictionary<string, Ty>)>();

            foreach (var x in GenericFunctions)
            {
                // different name = not possible
                if (x.Name != name) continue;
                // different argcount = not possible
                if (x.Predicate.InnerTypes.Length != type.InnerTypes.Length + 1) continue;

                var TypeArgs = new Dictionary<string, Ty>();

                for (int i = 0; i < type.InnerTypes.Length; i++)
                {
                    if (!x.Predicate.InnerTypes[i].TryMonomorphize(type.InnerTypes[i], ref TypeArgs, x.Typeargs))
                        continue;
                }
                // If we get here the function is a valid candidate for monomorphization
                Console.WriteLine("Found a valid candidate");
                ValidFunctions.Add((x, TypeArgs));
            }

            if (ValidFunctions.Count > 1)
                ErrorReporter.ErrorFL("Multiple generic variants for function {0}", FileAndLine, name);

            if (ValidFunctions.Count == 0) // return false - no possible candidate
                return null;

            // Now we have only one valid option, so actually monomorphize it
            var MonomorphizedPredicate = (FunctionType)
                ValidFunctions[0].Item1.Predicate.Monomorphize(ValidFunctions[0].Item2);

            Console.WriteLine("Monomorphized the candidate: {0}", MonomorphizedPredicate);

            // Deep-copy the overloads - this prevents different monomorphized variants from affecting each other
            // TODO Somehow get rid of this slow copying
            // And generate the function
            var MonomorphizedFunction = new FunctionNode(
                ValidFunctions[0].Item1.Name,
                ValidFunctions[0].Item1.Overloads.Select(
                    x => (x.Item1.Map(patt => patt.Clone(ValidFunctions[0].Item2)), 
                        x.Item2.Clone(ValidFunctions[0].Item2), 
                        (WhereClauseNode)x.Item3?.Clone(ValidFunctions[0].Item2))).ToArray(),
                MonomorphizedPredicate,
                null,
                ValidFunctions[0].Item1.FileAndLine
            );

            GlobalFunctions.Add((MonomorphizedFunction.Name, MonomorphizedPredicate));

            // Save the CurrentFunctionSymbols and then restore them
            var Backup = CurrentFunctionSymbols.ToDictionary((x) => x.Key, (x) => x.Value);
            VisitFunction(MonomorphizedFunction);
            CurrentFunctionSymbols = Backup;

            NewMonomorphizedFunctions.Add(MonomorphizedFunction);

            return MonomorphizedFunction;
        }
    }
}
