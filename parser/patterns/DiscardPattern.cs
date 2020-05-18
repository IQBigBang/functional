using System;
using Functional.types;

namespace Functional.parser.patterns
{
    public class DiscardPattern : Pattern
    {
        private AstType patternType;

        public string CompileTest(string baseName)
            => "true"; // matches anything

        public System.Collections.Immutable.ImmutableList<(string, string)> GetBindings(System.Collections.Immutable.ImmutableList<(string, string)> bindings, string baseName)
            => bindings;

        public System.Collections.Immutable.ImmutableList<(string, AstType)> GetBindingsTypes(System.Collections.Immutable.ImmutableList<(string, AstType)> bindings)
            => bindings;

        public bool MatchesType(AstType type)
        {
            patternType = type;
            return true;
        }
    }
}
