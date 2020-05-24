using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Functional.ast;
using Functional.debug;
using Functional.types;

namespace Functional.engines
{
    // Generates .h header fiels with `struct` definitions and `typedef`s
    // generates constructors for and-types and or-types
    // generates function prototypes
    public class HCompiler : AstVisitor
    {
        private StreamWriter Output;
        // prevents duplicite code
        private List<string> Generated;

        public HCompiler(StreamWriter outputWriter)
        {
            Output = outputWriter;
            Generated = new List<string>();
        }

        public static void GenerateFunctionPrototype(FunctionNode node, StreamWriter output)
        {
            // Main function always returns `nil` which is defined as always zero
            if (node.Name == "main")
            {
                output.Write("int main()");
                return;
            }
            
            var innerTypes = node.Predicate.InnerTypes;
            output.Write("{0} {1}(", innerTypes.Last().GetCName(), node.GetMangledName());
            for (int i = 0; i < innerTypes.Length - 1; i++)
            {
                output.Write("{0} _{1}", innerTypes[i].GetCName(), i);
                if (i != innerTypes.Length - 2) output.Write(", ");
            }
            output.Write(")");
        }

        private void GenerateStructDefinition(Ty type)
        {
            if (type.Type.Is<FunctionType>())
            {
                var ftype = type.Type.As<FunctionType>();
                // Note that function types may or may not be used
                if (Generated.Contains(ftype.GetCName()))
                    return;
                // First generate inner types
                Array.ForEach(ftype.InnerTypes, GenerateStructDefinition);
                // Then the typedef (for function pointers)
                Output.Write("typedef {0}", ftype.InnerTypes.Last().GetCName());
                Output.Write("(*{0})", ftype.GetCName());
                // Take all otherTypes except last one = return value
                var otherTypes = ftype.InnerTypes
                    .Take(ftype.InnerTypes.Length - 1)
                    .Select((tp) => tp.GetCName());
                Output.WriteLine("({0});", string.Join(", ", otherTypes));

                Generated.Add(ftype.GetCName());
            }
        }

        public void VisitAll((List<FunctionNode>, List<TypeDefinitionNode>) nodes)
        {
            // Generate typedefs, in case of self-recursive types
            nodes.Item2.ForEach((tp) =>
                Output.WriteLine("typedef struct _Struct{0}_t {0}_t;", tp.Name));
            nodes.Item2.ForEach(Visit);
            nodes.Item1.ForEach(Visit);
        }

        // We check types of all nodes
        public new void Visit(Node node)
        {
            if (!(node is FunctionNode) && !(node.NodeType is null))
                GenerateStructDefinition(node.NodeType);
            node.Accept(this);
        }

        public override void VisitBinOp(BinOpNode node)
        {
            Visit(node.Lhs);
            Visit(node.Rhs);
        }

        public override void VisitCall(CallNode node)
        {
            Visit(node.Callee);
            Array.ForEach(node.Args, Visit);
        }

        public override void VisitConstant(ConstantNode node) { }

        public override void VisitFunction(FunctionNode node)
        {
            if (node.IsExternal)
            {
                Output.Write("extern ");
                GenerateFunctionPrototype(node, Output);
                Output.WriteLine(";");
                return;
            }

            Array.ForEach(node.Overloads, (x) => {
                Visit(x.Item2);
                if (!(x.Item3 is null)) Visit(x.Item3);
            });

            GenerateFunctionPrototype(node, Output);
            Output.WriteLine(";");
        }

        public override void VisitListNode(ListNode node)
        {
            Array.ForEach(node.Elements, Visit);
        }

        public override void VisitTypeDefinition(TypeDefinitionNode node)
        {
            if (node.ActualType is AndType atype)
            {
                if (Generated.Contains(node.Name))
                    return;

                // Generate typedef struct
                Output.WriteLine("typedef struct _Struct{0}_t {{", node.Name);
                for (int i = 0; i < atype.Members.Length; i++)
                    Output.WriteLine("{0} _{1};", atype.Members[i].GetCName(), i);
                Output.WriteLine("}} {0}_t;", node.Name);

                // Then generate the constructor
                var ConstructorParameters = new string[atype.Members.Length];
                for (int i = 0; i < atype.Members.Length; i++)
                    ConstructorParameters[i] = atype.Members[i].GetCName() + " _" + i;

                Output.WriteLine("{0}_t* {1}({2}) {{",
                    node.Name,
                    // we use the FunctionType only for mangled name generation, so we add `null` as a replacement for the return type which does not affect the mangled name
                    new FunctionType(atype.Members.Append(null).ToArray()).GetNamedFunctionMangledName(node.Name),
                    string.Join(", ", ConstructorParameters));

                Output.WriteLine("{0}_t* tmp = ({0}_t*)alloc(sizeof({0}_t));", node.Name);
                for (int i = 0; i < atype.Members.Length; i++)
                    Output.WriteLine("tmp->_{0} = _{0};", i);
                Output.WriteLine("return tmp;");
                Output.WriteLine("}");

                Generated.Add(node.Name);

            } else if (node.ActualType is OrType otype)
            {
                if (Generated.Contains(node.Name))
                    return;

                // Generate typedef struct (tagged union)
                Output.WriteLine("typedef struct _Struct{0}_t {{", node.Name);
                Output.WriteLine("int tag;");
                Output.WriteLine("union {");
                Array.ForEach(otype.Variants, (x) =>
                    Output.WriteLine("{0} {1};", x.Item2.GetCName(), x.Item1));
                Output.WriteLine("} as;");
                Output.WriteLine("}} {0}_t;", node.Name);

                // Generate variant constructors
                for (int i = 0; i < otype.Variants.Length; i++)
                {
                    var (variantName, variantType) = otype.Variants[i];

                    Output.WriteLine("{0}_t* {1}({2} val) {{",
                        node.Name,
                        // see AndType constructor generation, the same hack 
                        new FunctionType(new Ty[] { variantType, null }).GetNamedFunctionMangledName(variantName), 
                        variantType.GetCName());
                    Output.WriteLine("{0}_t* tmp = ({0}_t*)alloc(sizeof({0}_t));", node.Name);
                    Output.WriteLine("tmp->tag = {0};", i);
                    Output.WriteLine("tmp->as.{0} = val;", variantName);
                    Output.WriteLine("return tmp;");
                    Output.WriteLine("}");
                }

                Generated.Add(node.Name);
            }
        }

        public override void VisitVar(VarNode node) {}

        public override void VisitWhereClause(WhereClauseNode node)
        {
            Array.ForEach(node.Bindings, (x) => Visit(x.Item2));
        }
    }
}
