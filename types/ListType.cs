using System;
using System.Collections.Generic;

namespace Functional.types
{
    public class ListType : AstType
    {
        public AstType ListElementsType { get; }

        public ListType(AstType listElementsType)
        {
            ListElementsType = listElementsType;
        }

        public override string GetCName()
            => "list_t*";

        public override string GetMangledName()
            => "L";

        public override string ToString()
            => "List " + ListElementsType;

        public override void ResolveNamedTypes(Dictionary<string, AstType> aliases)
            => ListElementsType.ResolveNamedTypes(aliases);

        public bool Equals(ListType lt)
            => ListElementsType == lt.ListElementsType;
    }
}
