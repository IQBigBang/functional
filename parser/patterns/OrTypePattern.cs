using System;
using System.Linq;
using Functional.types;

namespace Functional.parser.patterns
{
    public class OrTypePattern : Pattern
    {
        public string VariantName { get; }
        public Pattern InnerValue { get; }
        private AstType patType;

        public OrTypePattern(string variantName, Pattern innerValue)
        {
            VariantName = variantName;
            InnerValue = innerValue;
        }

        public string CompileTest(string baseName)
        {
            var VariantIndex = patType.As<OrType>().Variants
                                    .Select((item, i) => new { Item = item, Index = i })
                                    .First(x => x.Item.Item1 == VariantName).Index;
            var InnerValueTest = InnerValue.CompileTest(baseName + "->as." + VariantName);
            return baseName + "->tag == " + VariantIndex + " && " + InnerValueTest;
        }

        public bool MatchesType(AstType type)
        {
            if (!type.Is<OrType>()) return false;
            var ortype = type.As<OrType>();

            patType = type;
            return ortype.Variants.Any((variant) =>
                variant.Item1 == VariantName && InnerValue.MatchesType(variant.Item2));
        }

        public System.Collections.Immutable.ImmutableList<(string, AstType)> GetBindingsTypes(System.Collections.Immutable.ImmutableList<(string, AstType)> bindings)
            => InnerValue.GetBindingsTypes(bindings);

        public System.Collections.Immutable.ImmutableList<(string, string)> GetBindings(System.Collections.Immutable.ImmutableList<(string, string)> bindings, string baseName)
            => InnerValue.GetBindings(bindings, baseName + "->as." + VariantName);
    }
}
