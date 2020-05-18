using System;
using System.Collections.Generic;
using System.Linq;

namespace Functional.types
{
    public class AndType : AstType
    {
        public AstType[] Members { get; }

        public AndType(AstType[] members)
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

        public override void ResolveNamedTypes(Dictionary<string, AstType> aliases)
            => Array.ForEach(Members, (x) => x.ResolveNamedTypes(aliases));
    }
}
