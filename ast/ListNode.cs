using System;
using System.Linq;
using Functional.types;

namespace Functional.ast
{
    public class ListNode : Node
    {
        public readonly Node[] Elements;
        // Warning: TypeHint may be null
        public readonly Ty TypeHint;

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

        public override Node Clone() => new ListNode(Elements.DeepClone(), TypeHint, FileAndLine);
    }
}
