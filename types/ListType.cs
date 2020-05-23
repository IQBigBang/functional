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
    }
}
