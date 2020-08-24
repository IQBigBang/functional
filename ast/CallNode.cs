using System;
using System.Collections.Generic;
using System.Linq;
using Functional.types;

namespace Functional.ast
{
    public class CallNode : Node
    {
        public Node Callee { get; }
        public Node[] Args { get; }

        public CallNode(Node callee, Node[] args, string fileAndLine) : base(fileAndLine)
        {
            Callee = callee;
            Args = args;
        }

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitCall(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitCall(this);
        }

        public override Node Clone() => new CallNode(Callee.Clone(), Args.DeepClone(), FileAndLine);
    }
}
