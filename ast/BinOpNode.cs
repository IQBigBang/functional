using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.ast
{
    [Serializable]
    public class BinOpNode : Node
    {
        public Node Lhs { get; }
        public string Op { get; } // +, -, *
        public Node Rhs { get; }

        public BinOpNode(Node lhs, string op, Node rhs, string fileAndLine) : base(fileAndLine)
        {
            Lhs = lhs;
            Op = op;
            Rhs = rhs;
        }

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitBinOp(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitBinOp(this);
        }
    }
}
