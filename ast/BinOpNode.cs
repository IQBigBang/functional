using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.ast
{
    public class BinOpNode : Node
    {
        public readonly Node Lhs;
        public readonly string Op; // +, -, *
        public readonly Node Rhs;

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

        public override Node Clone(Dictionary<string, Ty> newTypes) => new BinOpNode(Lhs.Clone(newTypes), Op, Rhs.Clone(newTypes), FileAndLine);
    }
}
