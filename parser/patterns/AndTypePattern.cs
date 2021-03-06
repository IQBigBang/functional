﻿using System;
using System.Collections.Generic;
using System.Linq;
using Functional.ast;
using Functional.types;

namespace Functional.parser.patterns
{
    public class AndTypePattern : Pattern
    {
        public readonly Pattern[] Members;
        private AndType patType;

        public AndTypePattern(Pattern[] members)
        {
            Members = members;
        }

        public bool MatchesType(Ty asttype)
        {
            if (!asttype.Type.Is<AndType>()) return false;
            AndType type = asttype.Type.As<AndType>();

            return (type.Members.Length == Members.Length)
                && type.Members.Zip(Members, (t, pat) => pat.MatchesType(t))
                               .Aggregate(true, (b1, b2) => b1 && b2);
        }

        public void SetType(Ty type)
        {
            patType = type.Type.As<AndType>();
            for (int i = 0; i < Members.Length; i++)
                Members[i].SetType(patType.Members[i]);
        }

        public string CompileTest(string baseName)
        {
            string[] Tests = new string[Members.Length];
            for (int i = 0; i < Members.Length; i++)
                Tests[i] = Members[i].CompileTest(baseName + "->_" + i);
            return string.Join(" && ", Tests);
        }

        public void GetBindingsTypes(ref Dictionary<string, Ty> bindings)
        {
            foreach (var patt in Members)
                patt.GetBindingsTypes(ref bindings);
        }

        public void GetBindings(ref Dictionary<string, string> bindings, string baseName)
        {
            for (int i = 0; i < Members.Length; i++)
                Members[i].GetBindings(ref bindings, baseName + "->_" + i);
        }

        public Pattern Clone(Dictionary<string, Ty> newTypes) => new AndTypePattern(Members.Map(x => x.Clone(newTypes)));
    }
}
