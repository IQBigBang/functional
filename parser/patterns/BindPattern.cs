using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.parser.patterns
{
    public class BindPattern : Pattern
    {
        public string bindname { get; }
        private Ty patternType;

        public BindPattern(string bindname)
        {
            this.bindname = bindname;
        }

        // matches any type
        public bool MatchesType(Ty type)
            => true;

        public void SetType(Ty type)
            => patternType = type;

        public string CompileTest(string baseName)
            => "true"; // BindPattern matches everything

        public void GetBindingsTypes(ref Dictionary<string, Ty> bindings)
        {
            bindings.Add(bindname, patternType);
        }

        public void GetBindings(ref Dictionary<string, string> bindings, string baseName)
        {
            bindings.Add(bindname, baseName);
        }

        public Pattern Clone() => new BindPattern(bindname);
    }
}
