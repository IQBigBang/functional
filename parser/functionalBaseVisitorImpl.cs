using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Functional.ast;
using Functional.types;
using Functional.parser.patterns;
using Functional.debug;
using Antlr4.Runtime;

namespace Functional.parser
{
    public class functionalBaseVisitorImpl : functionalBaseVisitor<object>
    {
        private string FileName;

        private TypeTable typeTable;

        private string FileAndLine(ParserRuleContext ctx)
            => FileName + ":" + ctx.Start.Line;

        public functionalBaseVisitorImpl(string fileName, ref TypeTable tt)
        {
            FileName = fileName;
            typeTable = tt;
        }

        // program

        public new(List<FunctionNode>, List<TypeDefinitionNode>) VisitProgram([NotNull] functionalParser.ProgramContext context)
        {
            var funcs = new List<FunctionNode>();
            var aliases = new List<TypeDefinitionNode>();

            foreach (var d in context.definition())
            {
                var def = Visit(d);
                if (def is null) continue;
                else if (def is FunctionNode fdef) funcs.Add(fdef);
                else if (def is TypeDefinitionNode tdef)
                {
                    aliases.Add(tdef);
                    typeTable.Insert(tdef.Name, tdef.ActualType, tdef.FileAndLine);
                }
            }
            return (funcs, aliases);
        }

        // definition - returns a Node (FunctionNode or TypeDefinitionNode or null)

        // Those are used by Maat (the build system) and ignored by the compiler
        public override object VisitModuleDefinition([NotNull] functionalParser.ModuleDefinitionContext context)
            => null;
        public override object VisitImportDefinition([NotNull] functionalParser.ImportDefinitionContext context)
            => null;
        public override object VisitIncludeDefinition([NotNull] functionalParser.IncludeDefinitionContext context)
            => null;

        public override object VisitExternalFunctionDefinition([NotNull] functionalParser.ExternalFunctionDefinitionContext context)
        {
            var predicate = ((string, Ty))Visit(context.predicate());

            FunctionType functionPredicate;
            if (predicate.Item2.Type.Is<FunctionType>())
                functionPredicate = predicate.Item2.Type.As<FunctionType>();
            else
                // If the predicate is not a function type make it into one (e.g. main :: Int)
                functionPredicate = new FunctionType(new Ty[] { predicate.Item2 });
                
            return new FunctionNode(predicate.Item1, functionPredicate, FileAndLine(context));
        }

        public override object VisitFunctionDefinition([NotNull] functionalParser.FunctionDefinitionContext context)
        {
            var predicate = ((string, Ty))Visit(context.predicate());

            FunctionType functionPredicate;
            if (predicate.Item2.Type.Is<FunctionType>())
                functionPredicate = predicate.Item2.Type.As<FunctionType>();
            else
                // If the predicate is not a function type make it into one (e.g. main :: Int)
                functionPredicate = new FunctionType(new Ty[] { predicate.Item2 });

            var overloads = context.stmt().Select(
                (obj) => ((string, Pattern[], Node, WhereClauseNode))Visit(obj)).ToArray();

            // check if all functions are of the same name
            if (overloads.Any((obj) => obj.Item1 != predicate.Item1))
                ErrorReporter.Error("The function block `{0}` contains different functions", predicate.Item1);

            return new FunctionNode(
                predicate.Item1,
                overloads.Select((obj) => (obj.Item2, obj.Item3, obj.Item4)).ToArray(),
                functionPredicate,
                FileAndLine(context)
            );
        }

        public override object VisitTypeDefinition([NotNull] functionalParser.TypeDefinitionContext context)
        {
            return new TypeDefinitionNode(context.ID().GetText(), (AstType)Visit(context.definitiontypename()), FileAndLine(context));
        }

        // predicate - returns a tuple (string FuncName, Ty predicate)

        public override object VisitPredicate([NotNull] functionalParser.PredicateContext context)
        {
            Ty type = (Ty)Visit(context.anontypename());
            return (context.ID().GetText(), type);
        }

        // anontypename - returns Ty

        public override object VisitCompositeAnontypename([NotNull] functionalParser.CompositeAnontypenameContext context)
            => new Ty(
                new FunctionType(context.simpleanontypename().Select((x) => (Ty)Visit(x)).ToArray()),
                ref typeTable);

        public override object VisitSimpleanontypenameAnontypename([NotNull] functionalParser.SimpleanontypenameAnontypenameContext context)
            => Visit(context.simpleanontypename());

        // simpleanontypename - returns Ty

        public override object VisitNamedSimpleanontypename([NotNull] functionalParser.NamedSimpleanontypenameContext context)
            => new Ty(context.ID().GetText(), ref typeTable);

        public override object VisitAnontypenameSimpleanontypename([NotNull] functionalParser.AnontypenameSimpleanontypenameContext context)
            => Visit(context.anontypename());

        public override object VisitListSimpleanontypename([NotNull] functionalParser.ListSimpleanontypenameContext context)
            => new Ty(
                new ListType((Ty)Visit(context.simpleanontypename())),
                ref typeTable);

        // definitiontypename - returns AstType

        public override object VisitAndtypeDefinitiontypename([NotNull] functionalParser.AndtypeDefinitiontypenameContext context)
            => new AndType(context.definitionsimpletypename().Select((x) => (Ty)Visit(x)).ToArray());

        public override object VisitOrtypeDefinitiontypename([NotNull] functionalParser.OrtypeDefinitiontypenameContext context)
            => new OrType(context.ID().Zip(context.definitionsimpletypename(),
                    (id, variant) => (id.GetText(), (Ty)Visit(variant)))
                    .ToArray());

        // definitionsimpletypename - returns Ty

        public override object VisitNamedDefinitionsimpletypename([NotNull] functionalParser.NamedDefinitionsimpletypenameContext context)
            => new Ty(context.ID().GetText(), ref typeTable);

        public override object VisitListDefinitionsimpletypename([NotNull] functionalParser.ListDefinitionsimpletypenameContext context)
            => new Ty(
                new ListType((Ty)Visit(context.definitionsimpletypename())),
                ref typeTable);
       
        // stmt - returns a (string FuncName, Pattern[] patterns, Node body, WhereClauseNode|null whereclause)

        public override object VisitFuncDefStmt([NotNull] functionalParser.FuncDefStmtContext context)
        {
            var patterns = context.patt().Select((obj) => (Pattern)Visit(obj)).ToArray();
            return (context.ID().GetText(), patterns, (Node)Visit(context.expr()), (WhereClauseNode)Visit(context.whereclause()));
        }

        // whereclause - returns a WhereClauseNode

        public override object VisitWhereClause([NotNull] functionalParser.WhereClauseContext context)
        {
            var bindings = new (Pattern, Node)[context.bindpatt().Length];
            for (int i = 0; i < bindings.Length; i++)
                bindings[i] = ((Pattern)Visit(context.bindpatt(i)), (Node)Visit(context.expr(i)));
            return new WhereClauseNode(bindings, FileAndLine(context));
        }

        public override object VisitNoWhereClause([NotNull] functionalParser.NoWhereClauseContext context)
            => null;

        // bindpatt - returns a Pattern

        public override object VisitBindBindPattern([NotNull] functionalParser.BindBindPatternContext context)
            => new BindPattern(context.ID().GetText());

        public override object VisitDiscardBindPattern([NotNull] functionalParser.DiscardBindPatternContext context)
            => new DiscardPattern();

        public override object VisitAndTypeBindPattern([NotNull] functionalParser.AndTypeBindPatternContext context)
            => new AndTypePattern(context.bindpatt().Select((patt) => (Pattern)Visit(patt)).ToArray());

        public override object VisitOrTypeBindPattern([NotNull] functionalParser.OrTypeBindPatternContext context)
            => new OrTypePattern(context.ID().GetText(), (Pattern)Visit(context.bindpatt()));

        public override object VisitListBindPattern([NotNull] functionalParser.ListBindPatternContext context)
            => new ListPattern(context.bindpatt().Select((x) => (Pattern)Visit(x)).ToArray());

        // patt - returns a Pattern

        public override object VisitBindPattern([NotNull] functionalParser.BindPatternContext context)
            => new BindPattern(context.ID().GetText());

        public override object VisitDiscardPattern([NotNull] functionalParser.DiscardPatternContext context)
            => new DiscardPattern();

        public override object VisitAndTypePattern([NotNull] functionalParser.AndTypePatternContext context)
            => new AndTypePattern(context.patt().Select((patt) => (Pattern)Visit(patt)).ToArray());

        public override object VisitOrTypePattern([NotNull] functionalParser.OrTypePatternContext context)
            => new OrTypePattern(context.ID().GetText(), (Pattern)Visit(context.patt()));

        public override object VisitConstIntPattern([NotNull] functionalParser.ConstIntPatternContext context)
           => new ConstIntPattern(int.Parse(context.INT().GetText()));

        public override object VisitEmptyListPattern([NotNull] functionalParser.EmptyListPatternContext context)
            => new ListPattern();

        public override object VisitListPattern([NotNull] functionalParser.ListPatternContext context)
            => new ListPattern(context.patt().Select((x) => (Pattern) Visit(x)).ToArray());

        // expr - returns a Node

        public override object VisitJoinExpr([NotNull] functionalParser.JoinExprContext context)
            => new BinOpNode((Node)Visit(context.mathexpr()), ":", (Node)Visit(context.expr()), FileAndLine(context));

        public override object VisitMathexprExpr([NotNull] functionalParser.MathexprExprContext context)
            => Visit(context.mathexpr());

        // mathexpr - returns a Node

        public override object VisitPlusMathexpr([NotNull] functionalParser.PlusMathexprContext context)
            => new BinOpNode((Node)Visit(context.mathexpr()),"+",(Node)Visit(context.term()), FileAndLine(context));

        public override object VisitMinusMathexpr([NotNull] functionalParser.MinusMathexprContext context)
            => new BinOpNode((Node)Visit(context.mathexpr()), "-", (Node)Visit(context.term()), FileAndLine(context));

        public override object VisitTermMathexpr([NotNull] functionalParser.TermMathexprContext context)
            => Visit(context.term());

        // term - returns a Node

        public override object VisitTimesTerm([NotNull] functionalParser.TimesTermContext context)
            => new BinOpNode((Node)Visit(context.term()), "*", (Node)Visit(context.call()), FileAndLine(context));

        public override object VisitCallTerm([NotNull] functionalParser.CallTermContext context)
            => Visit(context.call());

        // call - returns a Node

        public override object VisitCallCall([NotNull] functionalParser.CallCallContext context)
        {
            Node callee = (Node)Visit(context.atom(0));
            Node[] args = context.atom().Skip(1).Select((arg) => (Node)Visit(arg)).ToArray();

            return new CallNode(callee, args, FileAndLine(context));
        }

        public override object VisitAtomCall([NotNull] functionalParser.AtomCallContext context)
            => Visit(context.atom());

        // atom - returns a Node

        public override object VisitIntAtom([NotNull] functionalParser.IntAtomContext context)
            => new ConstantNode(int.Parse(context.GetText()), FileAndLine(context), ref typeTable);

        public override object VisitVarAtom([NotNull] functionalParser.VarAtomContext context)
        {
            var id = context.ID().GetText();
            if (context.anontypename() is null)
                return new VarNode(id, null, FileAndLine(context));
            return new VarNode(id, (Ty)Visit(context.anontypename()), FileAndLine(context));
        }

        public override object VisitStringAtom([NotNull] functionalParser.StringAtomContext context)
            => new ConstantNode(context.GetText(), FileAndLine(context), ref typeTable);

        public override object VisitNilAtom([NotNull] functionalParser.NilAtomContext context)
            => new ConstantNode(FileAndLine(context), ref typeTable);

        public override object VisitListAtom([NotNull] functionalParser.ListAtomContext context)
        {
            var elements = context.expr().Select((x) => (Node)Visit(x)).ToArray();
            if (context.anontypename() is null)
                return new ListNode(elements, null, FileAndLine(context));
            return new ListNode(elements, (Ty)Visit(context.anontypename()), FileAndLine(context));
        }

        public override object VisitParenAtom([NotNull] functionalParser.ParenAtomContext context)
            => Visit(context.expr());
    }
}
