using System;
using System.Collections.Generic;
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

            return (type.Members.Length == Members.Length)
                && type.Members.Zip(Members, (t, pat) => pat.MatchesType(t))
                               .Aggregate(true, (b1, b2) => b1 && b2);
        }

        public void SetType(AstType type)
            => patType = type;

        public string CompileTest(string baseName)
        {
            string[] Tests = new string[Members.Length];
            for (int i = 0; i < Members.Length; i++)
                Tests[i] = Members[i].CompileTest(baseName + "->_" + i);
            return string.Join(" && ", Tests);
        }

        public void GetBindingsTypes(ref Dictionary<string, AstType> bindings)
        {
            foreach (var patt in Members)
                patt.GetBindingsTypes(ref bindings);
        }

        public void GetBindings(ref Dictionary<string, string> bindings, string baseName)
        {
            for (int i = 0; i < Members.Length; i++)
                Members[i].GetBindings(ref bindings, baseName + "->_" + i);
        }
    }
}
