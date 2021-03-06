﻿using System;
using System.Collections.Generic;

namespace Functional.types
{
    public class NilType : AstType
    {
        public override string GetCName()
            => "nil_t";

        public override string GetMangledName()
            => "N";

        public override string ToString()
            => "Nil";

        public override bool TryMonomorphize(AstType ExpectedType, ref Dictionary<string, Ty> TypeArgs, string[] ExpectedTypeArgs)
            => ExpectedType.Is<NilType>();

        public override AstType Monomorphize(Dictionary<string, Ty> TypeArgs)
            => this;
    }
}
