using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Functional.ast;
using Functional.debug;
using Functional.types;

namespace Functional.engines
{
    /// <summary>
    /// Compiles the syntax tree into a C source file
    /// 
    /// Note: While visiting a node returns just a string,
    /// the node may write to the output stream (e.g. temporary variables)
    /// Therefore, when a function does write to the stream, it should
    /// do so just once at the end of the function (or when a new scope begins) when 
    /// all children nodes have been evaluated.
    /// </summary>
    public class CCompiler : AstVisitor<string>
    {
        private StreamWriter Output;
        private Dictionary<string, string> CurrentFunctionSymbols;

        public CCompiler(StreamWriter outputWriter)
        {
            Output = outputWriter;
            CurrentFunctionSymbols = new Dictionary<string, string>();
        }

        private static Random rand = new Random();
        private const string CharArray = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";

        public static string GetRandomString()
        {
            string s = "";
            for (int i = 0; i < 8; i++)
                s += CharArray[rand.Next(CharArray.Length)];
            return s;
        }

        public void CompileAll((List<FunctionNode>, List<TypeDefinitionNode>) definitions)
        {
            definitions.Item1.ForEach((f) => Visit(f));
        }

        public override string VisitTypeDefinition(TypeDefinitionNode node)
        {
            return "";
        }

        public override string VisitBinOp(BinOpNode node)
        {
            if (node.Op == ":")
                return string.Format("list_cons({0}, {1})", Visit(node.Lhs), Visit(node.Rhs));
            if (node.Op == "+" && node.Lhs.NodeType.Type.Is<StringType>())
                return string.Format("string_concat({0}, {1})", Visit(node.Lhs), Visit(node.Rhs));
            // As of right now, integer operations (expect for ':') are supported. (checked by TypeChecker)
            return string.Format("({0}) {1} ({2})", Visit(node.Lhs), node.Op, Visit(node.Rhs));
        }

        public override string VisitCall(CallNode node)
            => Visit(node.Callee)
                    + "("
                    + string.Join(", ", node.Args.Select((x) => Visit(x)))
                    + ")";

        public override string VisitConstant(ConstantNode node)
        {
            if (node.NodeType.Type.Is<IntType>())
                return string.Format("{0}", (int)node.Value);
            if (node.NodeType.Type.Is<BoolType>())
                return string.Format("{0}", (bool)node.Value);
            if (node.NodeType.Type.Is<NilType>())
                return "nil";
            if (node.NodeType.Type.Is<StringType>())
                return string.Format("string_new_literal({0}, {1})",
                    (string)node.Value, ((string)node.Value).Length - 2);

            node.ReportError("Invalid constant type. Expected Int, Bool or Nil");
            return "";
        }

        public override string VisitFunction(FunctionNode node) 
        {
            // Note: here the frequent writing is okay, because we don't Visit() any nodes

            if (node.IsExternal)
                return "";

            HCompiler.GenerateFunctionPrototype(node, Output);
            // Open the block
            Output.WriteLine(" {");

            if (node.Name == "main")
                Output.WriteLine("GC_init();");

            foreach (var overload in node.Overloads)
            {
                // First resolve bindings
                CurrentFunctionSymbols.Clear();

                for (int i = 0; i < overload.Item1.Length; i++)
                {
                    overload.Item1[i].GetBindings(ref CurrentFunctionSymbols, "_" + i);
                }

                // Then insert pattern tests
                if (overload.Item1.Length > 0)
                {
                    Output.Write("if (");
                    var Tests = new string[overload.Item1.Length];
                    for (int i = 0; i < Tests.Length; i++)
                        Tests[i] = overload.Item1[i].CompileTest("_" + i);
                    Output.Write(string.Join(" && ", Tests));
                    Output.WriteLine(") {");
                }

                // Then evaluate 'where' clause
                if (!(overload.Item3 is null))
                {
                    Visit(overload.Item3);
                }

                // Now compile the block
                Output.WriteLine("return {0};", Visit(overload.Item2));

                // Close the if
                if (overload.Item1.Length > 0)
                    Output.WriteLine("}");
            }
            // Close the function
            Output.WriteLine("}");
            return "";
        }

        public override string VisitListNode(ListNode node)
        {
            var values = node.Elements.Select(Visit).ToArray();

            // If less than five elements, use a standard function for creation
            if (node.Elements.Length == 0)
                return "list_new()";
            if (node.Elements.Length <= 4)
                return string.Format("list_new{0}({1})", node.Elements.Length,
                    string.Join(", ", values));

            // Otherwise we use a list of `cons` (probably slower)
            node.ReportWarning("List literals with more than 4 elements might be slower to construct");
            return
                values.Reverse().Aggregate("list_new()",
                    (acc, val) => "list_cons(" + val + ", " + acc + ")");
        }

        public override string VisitVar(VarNode node)
        {
            if (CurrentFunctionSymbols.ContainsKey(node.Name))
                return CurrentFunctionSymbols[node.Name];
            // If the variable is not a local symbol, it must be a global variable
            // in which case, its name is already mangled
            return node.Name;
        }

        public override string VisitWhereClause(WhereClauseNode node)
        {
            foreach (var (patt, val) in node.Bindings)
            {
                // Compile the value
                string value = Visit(val);
                // Generate a random (kind-of) name
                string variableName = "_where" + GetRandomString();
                Output.WriteLine("{0} {1} = {2};", val.NodeType.GetCName(), variableName, value);

                // Resolve bindings
                patt.GetBindings(ref CurrentFunctionSymbols, variableName);
            }
            return "";
        }
    }
}
