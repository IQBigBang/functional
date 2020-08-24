using System;
using System.Collections.Generic;

namespace Functional.types
{
    public class ListType : AstType
    {
        public Ty ListElementsType { get; }

        public ListType(Ty listElementsType)
        {
            ListElementsType = listElementsType;
        }

        public override string GetCName()
            => "list_t*";

        public override string GetMangledName()
            => "L_" + ListElementsType.GetMangledName();

        public override string ToString()
            => "List " + ListElementsType;

        public bool Equals(ListType lt)
            => ListElementsType == lt.ListElementsType;

        public override bool TryMonomorphize(AstType ExpectedType, ref Dictionary<string, Ty> TypeArgs, string[] ExpectedTypeArgs)
        {
            if (!ExpectedType.Is<ListType>())
                return false;
            return ListElementsType.TryMonomorphize(ExpectedType.As<ListType>().ListElementsType, ref TypeArgs, ExpectedTypeArgs);
        }

        public override AstType Monomorphize(Dictionary<string, Ty> TypeArgs)
            => new ListType(ListElementsType.Monomorphize(TypeArgs));
    }
}
