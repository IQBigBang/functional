using System;
using System.Collections.Generic;
using System.Linq;
using Functional.engines;
using Functional.parser.patterns;
using Functional.types;

namespace Functional.ast
{
    public class FunctionNode : Node
    {
        public string Name { get; }
        public bool IsExternal { get; }
        public (Pattern[], Node, WhereClauseNode)[] Overloads { get; }

        public FunctionType Predicate;

        public FunctionNode(string name, FunctionType predicate, string fileAndLine) : base(fileAndLine)
        {
            Name = name;
            IsExternal = true;
            Overloads = null;
            Predicate = predicate;
        }

        public FunctionNode(string name, (Pattern[], Node, WhereClauseNode)[] overloads, FunctionType predicate, string fileAndLine) : base(fileAndLine)
        {
            Name = name;
            IsExternal = false;
            Overloads = overloads;
            Predicate = predicate;
        }

        public string GetMangledName()
        {
            var s = "_M" + Name;
            // Skip the last type = return type
            for (int i = 0; i < Predicate.InnerTypes.Length - 1; i++)
                s += "_" + Predicate.InnerTypes[i].GetMangledName() + i;
            return s;
        }

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitFunction(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitFunction(this);
        }
    }
}
