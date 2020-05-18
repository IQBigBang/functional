using System;
using System.Linq;
using Functional.types;

namespace Functional.parser.patterns
{
    public class AndTypePattern : Pattern
    {
        public Pattern[] Members { get; }
        private AstType patType;

        public AndTypePattern(Pattern[] members)
        {
            Members = members;
        }

        public bool MatchesType(AstType asttype)
        {
            if (!asttype.Is<AndType>()) return false;
            AndType type = asttype.As<AndType>();

            patType = type;
            return (type.Members.Length == Members.Length)
                && type.Members.Zip(Members, (t, pat) => pat.MatchesType(t))
                               .Aggregate(true, (b1, b2) => b1 && b2);
        }

        public string CompileTest(string baseName)
        {
            string[] Tests = new string[Members.Length];
            for (int i = 0; i < Members.Length; i++)
                Tests[i] = Members[i].CompileTest(baseName + "->_" + i);
            return string.Join(" && ", Tests);
        }

        public System.Collections.Immutable.ImmutableList<(string, AstType)> GetBindingsTypes(System.Collections.Immutable.ImmutableList<(string, AstType)> bindings)
            => Members.Aggregate(bindings, (bind, patt) => patt.GetBindingsTypes(bind));

        public System.Collections.Immutable.ImmutableList<(string, string)> GetBindings(System.Collections.Immutable.ImmutableList<(string, string)> bindings, string baseName)
        {
            var Bindings = bindings;
            for (int i = 0; i < Members.Length; i++)
                Bindings = Members[i].GetBindings(Bindings, baseName + "->_" + i);
            return Bindings;
        }
    }
}
