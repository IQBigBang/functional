using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.parser.patterns
{
    public class ConstIntPattern : Pattern
    {
        public readonly int Value;

        public ConstIntPattern(int constval)
        {
            Value = constval;
        }

        // matches only against integers
        public bool MatchesType(Ty type) => type.Type.Is<IntType>();

        public void SetType(Ty type) { }

        public string CompileTest(string baseName)
            => baseName + " == " + Value;

        public void GetBindingsTypes(ref Dictionary<string, Ty> bindings)
        {
        }

        public void GetBindings(ref Dictionary<string, string> bindings, string baseName)
        {
        }

        public Pattern Clone() => new ConstIntPattern(Value);
    }
}
