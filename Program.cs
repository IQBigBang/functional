using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Functional.parser;
using Functional.engines;
using System.IO;
using System.Diagnostics;
using Functional.ast;
using System.Linq;
using Functional.types;
using Functional.debug;
using System.Threading;
using CommandLine;

namespace Functional
{
    class MainClass
    {

        public class CmdArgs
        {
            [Value(0, Required = true)]
            public IEnumerable<string> Files { get; set; }

            [Option('m', "module", Required = true)]
            public string Module { get; set; }

            [Option('b', "builddir", Required = true)]
            public string BuildDir { get; set; }

            [Option('i', "imports", Default = "", Required = false)]
            public string Imports { get; set; }
        }

        public static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<CmdArgs>(args)
                .WithParsed(Evaluate);
        }

        public static void Evaluate(CmdArgs args)
        {
            ErrorReporter.ThrowExceptions = true;

            // All the files are processed together
            var AllFunctions = new List<FunctionNode>();
            var AllTypes = new List<TypeDefinitionNode>();

            foreach (var v in args.Files)
            {
                var (fs, ts) = BuildAST(Parse(v));
                AllFunctions.AddRange(fs);
                AllTypes.AddRange(ts);
            }
            
            TypeCheck((AllFunctions, AllTypes));
            Optimize(AllFunctions);

            using (var h_output = new StreamWriter(File.Open(Path.Combine(args.BuildDir, args.Module + ".h"), FileMode.Create, FileAccess.ReadWrite)))
            {
                h_output.WriteLine("#ifndef _{0}_h", args.Module);
                h_output.WriteLine("#define _{0}_h", args.Module);

                foreach (var i in args.Imports.Split(','))
                {
                    if (i.StartsWith("std.", 0))
                        h_output.WriteLine("#include \"../dist/std/{0}.h\"", i.Substring(4));
                    else
                        h_output.WriteLine("#include \"{0}\"", i + ".h");
                }

                CompileHeader((AllFunctions, AllTypes), h_output);
                h_output.WriteLine("#endif");
            }

            using (var c_output = new StreamWriter(File.Open(Path.Combine(args.BuildDir, args.Module + ".c"), FileMode.Create, FileAccess.ReadWrite)))
            {
                c_output.WriteLine("#include \"{0}\"", args.Module + ".h");
                Compile((AllFunctions, AllTypes), c_output);
            }

            using (var fh_output = new StreamWriter(File.Open(Path.Combine(args.BuildDir, args.Module + ".fh"), FileMode.Create, FileAccess.ReadWrite)))
            {
                CompileFHeader((AllFunctions, AllTypes), fh_output);
            }
        }

        public static functionalParser Parse(string filePath)
        {
            if (!File.Exists(filePath))
                ErrorReporter.Error("File {0} does not exist", filePath);

            AntlrInputStream inputStream = new AntlrInputStream(File.Open(filePath, FileMode.Open, FileAccess.Read));
            functionalLexer lexer = new functionalLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            return new functionalParser(commonTokenStream);
        }

        public static (List<FunctionNode>, List<TypeDefinitionNode>) BuildAST(functionalParser p)
        {
            functionalBaseVisitorImpl visitor = new functionalBaseVisitorImpl("main.f");
            return visitor.VisitProgram(p.program());
        }

        public static void TypeCheck((List<FunctionNode>, List<TypeDefinitionNode>) definitions)
        {
            new TypeChecker().CheckAll(definitions);
        }

        public static void Optimize(List<FunctionNode> functions)
        {
            return; // TODO
        }

        public static void CompileHeader((List<FunctionNode>, List<TypeDefinitionNode>) definitions, StreamWriter output)
        {
            new HCompiler(output).VisitAll(definitions);
        }

        public static void Compile((List<FunctionNode>, List<TypeDefinitionNode>) definitions, StreamWriter output)
        {
            new CCompiler(output).CompileAll(definitions);
        }

        public static void CompileFHeader((List<FunctionNode>, List<TypeDefinitionNode>) definitions, StreamWriter output)
        {
            // compile the .fh file
            foreach (var type in definitions.Item2)
            {
                output.WriteLine("type {0} = {1}", type.Name, type.NodeType);
            }
            foreach (var func in definitions.Item1)
            {
                output.WriteLine("external {0} :: {1}", func.Name, func.NodeType);
            }
        }
    }
}
