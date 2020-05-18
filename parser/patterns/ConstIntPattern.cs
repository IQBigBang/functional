using System;
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

        public string CompileTest(string baseName)
            => baseName + " == " + value;

        public System.Collections.Immutable.ImmutableList<(string, AstType)> GetBindingsTypes(System.Collections.Immutable.ImmutableList<(string, AstType)> bindings)
            => bindings;

        public System.Collections.Immutable.ImmutableList<(string, string)> GetBindings(System.Collections.Immutable.ImmutableList<(string, string)> bindings, string baseName)
            => bindings;
    }
}
