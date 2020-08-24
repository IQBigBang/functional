using System;
using System.Collections.Generic;

namespace Functional.types
{
    public class IntType : AstType
    {
        public override string GetCName()
            => "int";

        public override string GetMangledName()
            => "I";

        public override string ToString()
            => "Int";

        public override bool TryMonomorphize(AstType ExpectedType, ref Dictionary<string, Ty> TypeArgs, string[] ExpectedTypeArgs)
            => ExpectedType.Is<IntType>();

        public override AstType Monomorphize(Dictionary<string, Ty> TypeArgs)
            => this;
    }
}
