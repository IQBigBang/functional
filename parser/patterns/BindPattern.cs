using System;
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
        {
            patternType = type;
            return true;
        }

        public string CompileTest(string baseName)
            => "true"; // BindPattern matches everything

        public System.Collections.Immutable.ImmutableList<(string, AstType)> GetBindingsTypes(System.Collections.Immutable.ImmutableList<(string, AstType)> bindings)
        {
            return bindings.Add((bindname, patternType));
        }

        public System.Collections.Immutable.ImmutableList<(string, string)> GetBindings(System.Collections.Immutable.ImmutableList<(string, string)> bindings, string baseName)
        {
            return bindings.Add((bindname, baseName));
        }
    }
}
