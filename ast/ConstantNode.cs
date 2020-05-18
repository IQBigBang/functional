using System;
using Functional.types;

namespace Functional.ast
{
    public class ConstantNode : Node
    {
        public dynamic Value { get; }

        public ConstantNode(int value, string fileAndLine) : base(fileAndLine)
        {
            Value = value;
            NodeType = new NamedType("Int", new IntType());
        }

        public ConstantNode(bool value, string fileAndLine) : base(fileAndLine)
        {
            Value = value;
            NodeType = new NamedType("Bool", new BoolType());
        }

        // The string is expected to be in double quotes
        public ConstantNode(string s, string fileAndLine) : base(fileAndLine)
        {
            Value = s;
            NodeType = new NamedType("String", new StringType());
        }

        public ConstantNode(string fileAndLine) : base(fileAndLine)
        {
            Value = null;
            NodeType = new NamedType("Nil", new NilType());
        }

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitConstant(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitConstant(this);
        }
    }
}
