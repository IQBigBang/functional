using System;
using System.Collections.Generic;

namespace Functional.types
{
    public class StringType : AstType
    {
        public override string GetCName()
            => "string_t*";

        public override string GetMangledName()
            => "S";
            
        public override string ToString()
            => "String";
    }
}
