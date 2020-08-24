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

        public string GetNamedFunctionMangledName(string name)
        {
            var s = "_M" + name;
            // Skip the last type = return type
            for (int i = 0; i < InnerTypes.Length - 1; i++)
                s += "_" + InnerTypes[i].GetMangledName() + i;
            return s;
        }

        public bool Equals(FunctionType ft)
            => InnerTypes.Zip(ft.InnerTypes, (t1, t2) => t1 == t2)
                         .Aggregate(true, (b1, b2) => b1 && b2);

        public override string ToString()
        {
            return string.Join(" -> ", InnerTypes.Select((x) => 
            {
                if (x.MaybeType is null) return x.ToString(); // generic types
                if (x.Type.Is<FunctionType>())
                    return "(" + x + ")";
                return x.ToString();
            }));
        }

        public override bool TryMonomorphize(AstType ExpectedType, ref Dictionary<string, Ty> TypeArgs, string[] ExpectedTypeArgs)
        {
            if (!ExpectedType.Is<FunctionType>()) return false;
            var ftype = ExpectedType.As<FunctionType>();
            if (ftype.InnerTypes.Length != InnerTypes.Length) return false;
            for (int i = 0; i < InnerTypes.Length; ++i)
            {
                if (!InnerTypes[i].TryMonomorphize(ftype.InnerTypes[i], ref TypeArgs, ExpectedTypeArgs))
                    return false;
            }
            return true;
        }

        public override AstType Monomorphize(Dictionary<string, Ty> TypeArgs)
            => new FunctionType(InnerTypes.Select((x) => x.Monomorphize(TypeArgs)).ToArray());
    }
}
