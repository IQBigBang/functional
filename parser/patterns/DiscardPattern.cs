using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.parser.patterns
{
    public class DiscardPattern : Pattern
    {
        public string CompileTest(string baseName)
            => "true"; // matches anything

        public void GetBindings(ref Dictionary<string, string> bindings, string baseName)
        {
        }

        public void GetBindingsTypes(ref Dictionary<string, Ty> bindings)
        {
        }

        public bool MatchesType(Ty type)
            => true;

        public void SetType(Ty type) { }
        public Pattern Clone() => new DiscardPattern();
    }
}
