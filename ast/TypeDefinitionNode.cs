using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.ast
{
    public class TypeDefinitionNode : Node
    {
        public string Name { get; }

        public TypeDefinitionNode(string name, AstType actual, string fileAndLine) : base(fileAndLine)
        {
            Name = name;
            NodeType = actual;
        }

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitTypeDefinition(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitTypeDefinition(this);
        } 
    }
}
