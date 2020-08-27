using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.ast
{
    public class ConstantNode : Node
    {
        public readonly dynamic Value;

        public ConstantNode(int value, string fileAndLine, ref TypeTable tt) : base(fileAndLine)
        {
            Value = value;
            NodeType = new Ty("Int", ref tt);
        }

        public ConstantNode(bool value, string fileAndLine, ref TypeTable tt) : base(fileAndLine)
        {
            Value = value;
            NodeType = new Ty("Bool", ref tt);
        }

        // The string is expected to be in double quotes
        public ConstantNode(string s, string fileAndLine, ref TypeTable tt) : base(fileAndLine)
        {
            Value = s;
            NodeType = new Ty("String", ref tt);
        }

        public ConstantNode(string fileAndLine, ref TypeTable tt) : base(fileAndLine)
        {
            Value = null;
            NodeType = new Ty("Nil", ref tt);
        }

        private ConstantNode(dynamic value, Ty nodeType, string fileAndLine) : base(fileAndLine)
        {
            Value = value;
            NodeType = nodeType;
        }

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitConstant(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitConstant(this);
        }

        public override Node Clone(Dictionary<string, Ty> newTypes) => new ConstantNode(Value, NodeType, FileAndLine);
    }
}
