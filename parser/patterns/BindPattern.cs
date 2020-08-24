using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.parser.patterns
{
    public class BindPattern : Pattern
    {
        public readonly string BindName;
        private Ty patternType;

        public BindPattern(string bindname)
        {
            BindName = bindname;
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
            bindings.Add(BindName, patternType);
        }

        public void GetBindings(ref Dictionary<string, string> bindings, string baseName)
        {
            bindings.Add(BindName, baseName);
        }

        public Pattern Clone() => new BindPattern(BindName);
    }
}
