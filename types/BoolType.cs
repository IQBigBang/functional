﻿using System;
using System.Collections.Generic;

namespace Functional.types
{
    public class BoolType : AstType
    {
        public override string GetCName()
            => "bool";

        public override string GetMangledName()
            => "B";

        public override void ResolveNamedTypes(Dictionary<string, AstType> aliases)
        { }

        public override string ToString()
            => "Bool";
    }
}
