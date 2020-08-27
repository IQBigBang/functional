using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.ast
{
    public class TypeDefinitionNode : Node
    {
        public readonly string Name;
        public readonly AstType ActualType;

        public TypeDefinitionNode(string name, AstType actual, string fileAndLine) : base(fileAndLine)
        {
            Name = name;
            ActualType = actual;
        }

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitTypeDefinition(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitTypeDefinition(this);
        }

        public override Node Clone(Dictionary<string, Ty> newTypes) => throw new NotImplementedException(); // TODO: type generics
    }
}
