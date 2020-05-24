using System;
using Functional.types;

namespace Functional.ast
{
    public class ListNode : Node
    {
        public Node[] Elements { get; }
        // Warning: TypeHint may be null
        public Ty TypeHint { get; }

        public ListNode(Node[] elements, Ty typeHint, string fileAndLine) : base(fileAndLine)
        {
            Elements = elements;
            TypeHint = typeHint;
        }

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitListNode(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitListNode(this);
        }
    }
}
