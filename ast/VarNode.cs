using System;
using System.Linq;

namespace Functional.ast
{
    public class VarNode : Node
    {
        public string Name;

        public VarNode(string name, string fileAndLine) : base(fileAndLine)
        {
            Name = name;
        }

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitVar(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitVar(this);
        }
    }
}
