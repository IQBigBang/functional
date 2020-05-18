using System;
using System.Collections.Generic;
using Functional.debug;

namespace Functional.types
{
    public class NamedType : AstType
    {
        public AstType InnerType { get; private set; }
        public string Name { get; }

        public NamedType(string name)
        {
            Name = name;
        }

        public NamedType(string name, AstType innerType)
        {
            Name = name;
            InnerType = innerType;
        }

        public override string GetCName()
        {
            if (InnerType is IntType || InnerType is BoolType || InnerType is NilType || InnerType is StringType) 
                return InnerType.GetCName();
            return Name + "_t*";
        }

        public override string GetMangledName()
        {
            if (InnerType is IntType || InnerType is BoolType || InnerType is NilType || InnerType is StringType)
                return InnerType.GetMangledName();
            return Name + "_t";
        }

        public override void ResolveNamedTypes(Dictionary<string, AstType> aliases)
        {
            if (!aliases.ContainsKey(Name))
                ErrorReporter.Error("Unrecognized type `{0}`", Name);
            InnerType = aliases[Name];
        }

        public bool Equals(AstType at)
        {
            if (at is NamedType named)
                return Name == named.Name && InnerType == named.InnerType;
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
