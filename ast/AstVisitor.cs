using System;
namespace Functional.ast
{
    public abstract class AstVisitor
    {
        public void Visit(Node node) => node.Accept(this);
        public virtual void VisitBinOp(BinOpNode node) => throw new NotImplementedException();
        public virtual void VisitCall(CallNode node) => throw new NotImplementedException();
        public virtual void VisitConstant(ConstantNode node) => throw new NotImplementedException();
        public virtual void VisitFunction(FunctionNode node) => throw new NotImplementedException();
        public virtual void VisitListNode(ListNode node) => throw new NotImplementedException();
        public virtual void VisitTypeDefinition(TypeDefinitionNode node) => throw new NotImplementedException();
        public virtual void VisitVar(VarNode node) => throw new NotImplementedException();
        public virtual void VisitWhereClause(WhereClauseNode node) => throw new NotImplementedException();
    }

    public abstract class AstVisitor<T>
    {
        public T Visit(Node node) => node.Accept(this);
        public virtual T VisitBinOp(BinOpNode node) => throw new NotImplementedException();
        public virtual T VisitCall(CallNode node) => throw new NotImplementedException();
        public virtual T VisitConstant(ConstantNode node) => throw new NotImplementedException();
        public virtual T VisitFunction(FunctionNode node) => throw new NotImplementedException();
        public virtual T VisitListNode(ListNode node) => throw new NotImplementedException();
        public virtual T VisitTypeDefinition(TypeDefinitionNode node) => throw new NotImplementedException();
        public virtual T VisitVar(VarNode node) => throw new NotImplementedException();
        public virtual T VisitWhereClause(WhereClauseNode node) => throw new NotImplementedException();
    }
}
