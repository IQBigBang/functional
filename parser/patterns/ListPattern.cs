using System;
using System.Linq;
using Functional.types;

namespace Functional.parser.patterns
{
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

        public System.Collections.Immutable.ImmutableList<(string, string)> GetBindings(System.Collections.Immutable.ImmutableList<(string, string)> bindings, string baseName)
        {
            if (isEmpty) return bindings;
            var binds = ElementPatterns[0].GetBindings(bindings, "list_head(" + baseName + ")"); 
            for (int i = 1; i < ElementPatterns.Length; i++)
                binds = ElementPatterns[i].GetBindings(binds, "list_at(" + baseName + ", " + i + ")");

            if (ElementPatterns.Length == 1)
                return TailPattern.GetBindings(binds, "list_tail(" + baseName + ")");
            return TailPattern.GetBindings(binds, "list_tail_at(" + baseName + ", " + ElementPatterns.Length + ")");
        }

        public System.Collections.Immutable.ImmutableList<(string, AstType)> GetBindingsTypes(System.Collections.Immutable.ImmutableList<(string, AstType)> bindings)
        {
            if (isEmpty) return bindings;
            return TailPattern.GetBindingsTypes(
                ElementPatterns.Aggregate(bindings, (binds, pat) => pat.GetBindingsTypes(binds)));
        }

        public bool MatchesType(AstType type)
        {
            if (!type.Is<ListType>()) return false;
            ListType ltype = type.As<ListType>();

            if (isEmpty) return true;

            return ElementPatterns
                .Select((x) => x.MatchesType(ltype.ListElementsType))
                .Aggregate(true, (b1, b2) => b1 && b2)
                && TailPattern.MatchesType(ltype);
        } 
    }
}
