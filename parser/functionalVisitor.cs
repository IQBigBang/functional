//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from functional.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="functionalParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public interface IfunctionalVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="functionalParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] functionalParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ModuleDefinition</c>
	/// labeled alternative in <see cref="functionalParser.definition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitModuleDefinition([NotNull] functionalParser.ModuleDefinitionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ImportDefinition</c>
	/// labeled alternative in <see cref="functionalParser.definition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImportDefinition([NotNull] functionalParser.ImportDefinitionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IncludeDefinition</c>
	/// labeled alternative in <see cref="functionalParser.definition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIncludeDefinition([NotNull] functionalParser.IncludeDefinitionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ExternalFunctionDefinition</c>
	/// labeled alternative in <see cref="functionalParser.definition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExternalFunctionDefinition([NotNull] functionalParser.ExternalFunctionDefinitionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>FunctionDefinition</c>
	/// labeled alternative in <see cref="functionalParser.definition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionDefinition([NotNull] functionalParser.FunctionDefinitionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeDefinition</c>
	/// labeled alternative in <see cref="functionalParser.definition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeDefinition([NotNull] functionalParser.TypeDefinitionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="functionalParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPredicate([NotNull] functionalParser.PredicateContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>CompositeAnontypename</c>
	/// labeled alternative in <see cref="functionalParser.anontypename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompositeAnontypename([NotNull] functionalParser.CompositeAnontypenameContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>SimpleanontypenameAnontypename</c>
	/// labeled alternative in <see cref="functionalParser.anontypename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleanontypenameAnontypename([NotNull] functionalParser.SimpleanontypenameAnontypenameContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ListSimpleanontypename</c>
	/// labeled alternative in <see cref="functionalParser.simpleanontypename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListSimpleanontypename([NotNull] functionalParser.ListSimpleanontypenameContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>NamedSimpleanontypename</c>
	/// labeled alternative in <see cref="functionalParser.simpleanontypename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNamedSimpleanontypename([NotNull] functionalParser.NamedSimpleanontypenameContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AnontypenameSimpleanontypename</c>
	/// labeled alternative in <see cref="functionalParser.simpleanontypename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnontypenameSimpleanontypename([NotNull] functionalParser.AnontypenameSimpleanontypenameContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AndtypeDefinitiontypename</c>
	/// labeled alternative in <see cref="functionalParser.definitiontypename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAndtypeDefinitiontypename([NotNull] functionalParser.AndtypeDefinitiontypenameContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OrtypeDefinitiontypename</c>
	/// labeled alternative in <see cref="functionalParser.definitiontypename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOrtypeDefinitiontypename([NotNull] functionalParser.OrtypeDefinitiontypenameContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ListDefinitionsimpletypename</c>
	/// labeled alternative in <see cref="functionalParser.definitionsimpletypename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListDefinitionsimpletypename([NotNull] functionalParser.ListDefinitionsimpletypenameContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>NamedDefinitionsimpletypename</c>
	/// labeled alternative in <see cref="functionalParser.definitionsimpletypename"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNamedDefinitionsimpletypename([NotNull] functionalParser.NamedDefinitionsimpletypenameContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>FuncDefStmt</c>
	/// labeled alternative in <see cref="functionalParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncDefStmt([NotNull] functionalParser.FuncDefStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>WhereClause</c>
	/// labeled alternative in <see cref="functionalParser.whereclause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhereClause([NotNull] functionalParser.WhereClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>NoWhereClause</c>
	/// labeled alternative in <see cref="functionalParser.whereclause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNoWhereClause([NotNull] functionalParser.NoWhereClauseContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BindPattern</c>
	/// labeled alternative in <see cref="functionalParser.patt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBindPattern([NotNull] functionalParser.BindPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DiscardPattern</c>
	/// labeled alternative in <see cref="functionalParser.patt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDiscardPattern([NotNull] functionalParser.DiscardPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AndTypePattern</c>
	/// labeled alternative in <see cref="functionalParser.patt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAndTypePattern([NotNull] functionalParser.AndTypePatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OrTypePattern</c>
	/// labeled alternative in <see cref="functionalParser.patt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOrTypePattern([NotNull] functionalParser.OrTypePatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>EmptyListPattern</c>
	/// labeled alternative in <see cref="functionalParser.patt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEmptyListPattern([NotNull] functionalParser.EmptyListPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ListPattern</c>
	/// labeled alternative in <see cref="functionalParser.patt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListPattern([NotNull] functionalParser.ListPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstIntPattern</c>
	/// labeled alternative in <see cref="functionalParser.patt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstIntPattern([NotNull] functionalParser.ConstIntPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BindBindPattern</c>
	/// labeled alternative in <see cref="functionalParser.bindpatt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBindBindPattern([NotNull] functionalParser.BindBindPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DiscardBindPattern</c>
	/// labeled alternative in <see cref="functionalParser.bindpatt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDiscardBindPattern([NotNull] functionalParser.DiscardBindPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AndTypeBindPattern</c>
	/// labeled alternative in <see cref="functionalParser.bindpatt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAndTypeBindPattern([NotNull] functionalParser.AndTypeBindPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OrTypeBindPattern</c>
	/// labeled alternative in <see cref="functionalParser.bindpatt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOrTypeBindPattern([NotNull] functionalParser.OrTypeBindPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ListBindPattern</c>
	/// labeled alternative in <see cref="functionalParser.bindpatt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListBindPattern([NotNull] functionalParser.ListBindPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>JoinExpr</c>
	/// labeled alternative in <see cref="functionalParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJoinExpr([NotNull] functionalParser.JoinExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>MathexprExpr</c>
	/// labeled alternative in <see cref="functionalParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMathexprExpr([NotNull] functionalParser.MathexprExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>MinusMathexpr</c>
	/// labeled alternative in <see cref="functionalParser.mathexpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMinusMathexpr([NotNull] functionalParser.MinusMathexprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TermMathexpr</c>
	/// labeled alternative in <see cref="functionalParser.mathexpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTermMathexpr([NotNull] functionalParser.TermMathexprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PlusMathexpr</c>
	/// labeled alternative in <see cref="functionalParser.mathexpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPlusMathexpr([NotNull] functionalParser.PlusMathexprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>CallTerm</c>
	/// labeled alternative in <see cref="functionalParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCallTerm([NotNull] functionalParser.CallTermContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TimesTerm</c>
	/// labeled alternative in <see cref="functionalParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTimesTerm([NotNull] functionalParser.TimesTermContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>CallCall</c>
	/// labeled alternative in <see cref="functionalParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCallCall([NotNull] functionalParser.CallCallContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AtomCall</c>
	/// labeled alternative in <see cref="functionalParser.call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAtomCall([NotNull] functionalParser.AtomCallContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IntAtom</c>
	/// labeled alternative in <see cref="functionalParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIntAtom([NotNull] functionalParser.IntAtomContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>VarAtom</c>
	/// labeled alternative in <see cref="functionalParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarAtom([NotNull] functionalParser.VarAtomContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>StringAtom</c>
	/// labeled alternative in <see cref="functionalParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringAtom([NotNull] functionalParser.StringAtomContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>NilAtom</c>
	/// labeled alternative in <see cref="functionalParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNilAtom([NotNull] functionalParser.NilAtomContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TrueAtom</c>
	/// labeled alternative in <see cref="functionalParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTrueAtom([NotNull] functionalParser.TrueAtomContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>FalseAtom</c>
	/// labeled alternative in <see cref="functionalParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFalseAtom([NotNull] functionalParser.FalseAtomContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ParenAtom</c>
	/// labeled alternative in <see cref="functionalParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenAtom([NotNull] functionalParser.ParenAtomContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ListAtom</c>
	/// labeled alternative in <see cref="functionalParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListAtom([NotNull] functionalParser.ListAtomContext context);
}
