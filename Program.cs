﻿using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Functional.parser;
using Functional.engines;
using System.IO;
using Functional.ast;
using Functional.debug;
using CommandLine;
using Functional.types;

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

            [Option('i', "imports", Required = true)]
            public string Imports { get; set; }
        }

        public static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<CmdArgs>(args)
                .WithParsed(Evaluate);
        }

        public static void Evaluate(CmdArgs args)
        {
#if DEBUG
            ErrorReporter.ThrowExceptions = true;
#endif
            ErrorReporter.Init();

            // All the files are processed together
            var AllFunctions = new List<FunctionNode>();
            var AllGenericFunctions = new List<FunctionNode>();
            var AllTypes = new List<TypeDefinitionNode>();

            var GlobalTypeTable = new TypeTable();

            foreach (var v in args.Files)
            {
                var (fs, genericfs, ts) = BuildAST(Parse(v), ref GlobalTypeTable);
                AllFunctions.AddRange(fs);
                AllGenericFunctions.AddRange(genericfs);
                AllTypes.AddRange(ts);
            }

            // Update allfunctions with potentially new functions
            AllFunctions = 
            TypeCheck((AllFunctions, AllGenericFunctions, AllTypes), ref GlobalTypeTable);
            
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

        public static (List<FunctionNode>, List<FunctionNode>, List<TypeDefinitionNode>) BuildAST(functionalParser p, ref TypeTable tt)
        {
            functionalBaseVisitorImpl visitor = new functionalBaseVisitorImpl("main.f", ref tt);
            return visitor.VisitProgram(p.program());
        }

        /// <summary>
        /// The type-checker among other things resolves generic functions
        /// Therefore it takes a tuple (functions, genericfunctions, types)
        /// and returns just all functions including new ones
        /// </summary>
        public static List<FunctionNode> TypeCheck((List<FunctionNode>, List<FunctionNode>, List<TypeDefinitionNode>) definitions, ref TypeTable tt)
        {
            var tc = new TypeChecker();
            return tc.CheckAll(definitions, ref tt);
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
                output.WriteLine("type {0} = {1}", type.Name, type.ActualType);
            }
            foreach (var func in definitions.Item1)
            {
                // Not redefine already external functions 
                if (!func.IsExternal)
                    output.WriteLine("external {0} :: {1}", func.Name, func.Predicate);
            }
        }
    }
}
