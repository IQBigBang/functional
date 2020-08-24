using System;
using System.Collections.Generic;
using System.Linq;

namespace Functional.types
{
    public class OrType : AstType
    {
        public (string, Ty)[] Variants { get; }

        public OrType((string, Ty)[] variants)
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

        public bool Equals(OrType or)
            => Variants.Length == or.Variants.Length
               && Variants.Zip(or.Variants, (v1, v2) => v1.Item1 == v2.Item1 && v1.Item2 == v2.Item2)
                      .Aggregate(true, (b1, b2) => b1 && b2);

        public override bool TryMonomorphize(AstType ExpectedType, ref Dictionary<string, Ty> TypeArgs, string[] ExpectedTypeArgs)
        {
            if (!ExpectedType.Is<OrType>()) return false;
            var otype = ExpectedType.As<OrType>();
            if (otype.Variants.Length != Variants.Length) return false;
            for (int i = 0; i < Variants.Length; ++i)
            {
                if (Variants[i].Item1 != otype.Variants[i].Item1)
                    return false;
                if (!Variants[i].Item2.TryMonomorphize(otype.Variants[i].Item2, ref TypeArgs, ExpectedTypeArgs))
                    return false;
            }
            return true;
        }

        public override AstType Monomorphize(Dictionary<string, Ty> TypeArgs)
            => new OrType(Variants.Select((x) => (x.Item1, x.Item2.Monomorphize(TypeArgs))).ToArray());
    }
}
