using System;
using System.Collections.Generic;
using System.Linq;
using Functional.types;

namespace Functional.parser.patterns
{
    [Serializable]
    public class ListPattern : Pattern
    {
        // If true, the pattern matches against empty list (`[]`)
        public bool isEmpty { get; }

        // In form (x:y:z:rest), x, y and z are elementpatterns, rest is tailpattern
        public Pattern[] ElementPatterns;
        public Pattern TailPattern;

        public ListPattern()
        {
            isEmpty = true;
        }

        public ListPattern(Pattern[] Subpatterns)
        {
            isEmpty = false;
            ElementPatterns = Subpatterns.Take(Subpatterns.Length - 1).ToArray();
            TailPattern = Subpatterns.Last();
        }

        public string CompileTest(string baseName)
        {
            if (isEmpty) return "list_is_empty(" + baseName + ")";

            var ElementTests = string.Join(" && ", ElementPatterns
                .Select((x, i) => x.CompileTest("list_at(" + baseName + ", " + i + ")")));
            ElementTests += " && " + TailPattern.CompileTest("list_tail_at(" + baseName + ", " + ElementPatterns.Length + ")");

            return "list_has_n(" + baseName + ", " + ElementPatterns.Length + ") && " + ElementTests;
        }

        public bool MatchesType(Ty type)
        {
            if (!type.Type.Is<ListType>()) return false;
            ListType ltype = type.Type.As<ListType>();

            if (isEmpty) return true;

            return ElementPatterns
                .Select((x) => x.MatchesType(ltype.ListElementsType))
                .Aggregate(true, (b1, b2) => b1 && b2)
                && TailPattern.MatchesType(type);
        }

        public void SetType(Ty type) 
        {
            if (isEmpty) return;

            for (int i = 0; i < ElementPatterns.Length; i++)
                ElementPatterns[i].SetType(type.Type.As<ListType>().ListElementsType);
            TailPattern.SetType(type);
        }

        public void GetBindingsTypes(ref Dictionary<string, Ty> bindings)
        {
            if (isEmpty) return;
            foreach (var patt in ElementPatterns)
                patt.GetBindingsTypes(ref bindings);
            TailPattern.GetBindingsTypes(ref bindings);
        }

        public void GetBindings(ref Dictionary<string, string> bindings, string baseName)
        {
            if (isEmpty) return;

            ElementPatterns[0].GetBindings(ref bindings, "list_head(" + baseName + ")");
            for (int i = 1; i < ElementPatterns.Length; i++)
                ElementPatterns[i].GetBindings(ref bindings, "list_at(" + baseName + ", " + i + ")");

            if (ElementPatterns.Length == 1)
                TailPattern.GetBindings(ref bindings, "list_tail(" + baseName + ")");
            else
                TailPattern.GetBindings(ref bindings, "list_tail_at(" + baseName + ", " + ElementPatterns.Length + ")");
        }
    }
}
