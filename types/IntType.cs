﻿using System;
using System.Collections.Generic;

namespace Functional.types
{
    public class IntType : AstType
    {
        public override string GetCName()
            => "int";

        public override string GetMangledName()
            => "I";

        public override void ResolveNamedTypes(Dictionary<string, AstType> aliases)
        { }

        public override string ToString()
            => "Int";
    }
}
