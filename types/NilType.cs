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

        public override void ResolveNamedTypes(Dictionary<string, AstType> aliases)
        { }

        public override string ToString()
            => "Nil";
    }
}
