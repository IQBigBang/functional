using System;
using System.Collections.Generic;

namespace Functional.types
{
    public class BoolType : AstType
    {
        public override string GetCName()
            => "bool";

        public override string GetMangledName()
            => "B";

        public override string ToString()
            => "Bool";

        public override bool TryMonomorphize(AstType ExpectedType, ref Dictionary<string, Ty> TypeArgs, string[] ExpectedTypeArgs)
            => ExpectedType.Is<BoolType>();

        public override AstType Monomorphize(Dictionary<string, Ty> TypeArgs)
            => this;
    }
}
