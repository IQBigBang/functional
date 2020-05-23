using System;
using System.Collections.Generic;
using System.Linq;

namespace Functional.types
{
    public class FunctionType : AstType
    {
        // The last element = return type, before it are arguments
        public Ty[] InnerTypes { get; }

        public FunctionType(Ty[] innerTypes)
        {
            InnerTypes = innerTypes;
        }

        public override string GetCName()
            => GetMangledName();

        public override string GetMangledName()
        {
            var s = "F";
            for (int i = 0; i < InnerTypes.Length - 1; i++)
                s += "_" + InnerTypes[i].GetMangledName() + i;
            s += "_" + InnerTypes.Last().GetMangledName();
            return s;
        }

        public bool Equals(FunctionType ft)
            => InnerTypes.Zip(ft.InnerTypes, (t1, t2) => t1 == t2)
                         .Aggregate(true, (b1, b2) => b1 && b2);

        public override string ToString()
        {
            return string.Join(" -> ", InnerTypes.Select((x) => x.ToString()));
        }
    }
}
