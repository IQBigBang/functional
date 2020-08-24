using System;
using System.Collections.Generic;
using System.Linq;
using Functional.types;

namespace Functional.parser.patterns
{
    public class OrTypePattern : Pattern
    {
        public readonly string VariantName;
        public readonly Pattern InnerValue;
        private OrType patType;

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

        public bool MatchesType(Ty type)
        {
            if (!type.Type.Is<OrType>()) return false;
            var ortype = type.Type.As<OrType>();

            return ortype.Variants.Any((variant) =>
                variant.Item1 == VariantName && InnerValue.MatchesType(variant.Item2));
        }

        public void SetType(Ty type)
        {
            patType = type.Type.As<OrType>();
            InnerValue.SetType(patType.Variants.First((variant) => variant.Item1 == VariantName).Item2);
        }

        public void GetBindingsTypes(ref Dictionary<string, Ty> bindings)
        {
            InnerValue.GetBindingsTypes(ref bindings);
        }

        public void GetBindings(ref Dictionary<string, string> bindings, string baseName)
        {
            InnerValue.GetBindings(ref bindings, baseName + "->as." + VariantName);
        }

        public Pattern Clone() => new OrTypePattern(VariantName, InnerValue);
    }
}
