using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.parser.patterns
{
    public class BindPattern : Pattern
    {
        public string bindname { get; }
        private AstType patternType;

        public BindPattern(string bindname)
        {
            this.bindname = bindname;
        }

        // matches any type
        public bool MatchesType(AstType type)
            => true;

        public void SetType(AstType type)
            => patternType = type;

        public string CompileTest(string baseName)
            => "true"; // BindPattern matches everything

        public void GetBindingsTypes(ref Dictionary<string, AstType> bindings)
        {
            bindings.Add(bindname, patternType);
        }

        public void GetBindings(ref Dictionary<string, string> bindings, string baseName)
        {
            bindings.Add(bindname, baseName);
        }
    }
}
