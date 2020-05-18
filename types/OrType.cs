using System;
using System.Collections.Generic;
using System.Linq;

namespace Functional.types
{
    public class OrType : AstType
    {
        public (string, AstType)[] Variants { get; }

        public OrType((string, AstType)[] variants)
        {
            Variants = variants;
        }

        public override string GetCName()
            => throw new Exception(); // This should never happen, OrTypes should always be wrapped in NamedTypes

        public override string GetMangledName()
            => throw new Exception(); // see ^

        public override string ToString()
            => "(" + string.Join(" | ", 
                Variants.Select((x) => x.Item1 + " " + x.Item2)) 
                + ")";

        public override void ResolveNamedTypes(Dictionary<string, AstType> aliases)
            => Array.ForEach(Variants, (x) => x.Item2.ResolveNamedTypes(aliases));

        public bool Equals(OrType or)
            => Variants.Length == or.Variants.Length
               && Variants.Zip(or.Variants, (v1, v2) => v1.Item1 == v2.Item1 && v1.Item2 == v2.Item2)
                      .Aggregate(true, (b1, b2) => b1 && b2);
    }
}
