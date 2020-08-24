using System;
using System.Collections.Generic;
using System.Linq;

namespace Functional.types
{
    public class AndType : AstType
    {
        public Ty[] Members { get; }

        public AndType(Ty[] members)
        {
            Members = members;
        }

        public override string GetCName()
            => throw new Exception(); // This should never happen, AndTypes should always be wrapped in NamedTypes

        public override string GetMangledName()
            => throw new Exception(); // see ^

        public bool Equals(AndType at)
            => Members.Length == at.Members.Length 
               && Members.Zip(at.Members, (t1, t2) => t1 == t2)
                      .Aggregate(true, (b1, b2) => b1 && b2);

        public override string ToString()
        {
            return "(" + string.Join(" & ", Members.Select((x) => x.ToString())) + ")";
        }

        public override bool TryMonomorphize(AstType ExpectedType, ref Dictionary<string, Ty> TypeArgs, string[] ExpectedTypeArgs)
        {
            if (!ExpectedType.Is<AndType>()) return false;
            var atype = ExpectedType.As<AndType>();
            if (atype.Members.Length != Members.Length) return false;
            for (int i = 0; i < Members.Length; ++i)
            {
                if (!Members[i].TryMonomorphize(atype.Members[i], ref TypeArgs, ExpectedTypeArgs))
                    return false;
            }
            return true;
        }

        public override AstType Monomorphize(Dictionary<string, Ty> TypeArgs)
            => new AndType(Members.Select((x) => x.Monomorphize(TypeArgs)).ToArray());
    }
}
