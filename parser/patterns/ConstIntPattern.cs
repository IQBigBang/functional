using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.parser.patterns
{
    public class ConstIntPattern : Pattern
    {
        public int value { get; }

        public ConstIntPattern(int constval)
        {
            this.value = constval;
        }

        // matches only against integers
        public bool MatchesType(AstType type) => type.Is<IntType>();

        public void SetType(AstType type) { }

        public string CompileTest(string baseName)
            => baseName + " == " + value;

        public void GetBindingsTypes(ref Dictionary<string, AstType> bindings)
        {
        }

        public void GetBindings(ref Dictionary<string, string> bindings, string baseName)
        {
        }
    }
}
