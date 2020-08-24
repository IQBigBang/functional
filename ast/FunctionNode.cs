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
        public readonly string Name;

        public readonly bool IsExternal;
        // If function is external, this is null
        public readonly (Pattern[], Node, WhereClauseNode)[] Overloads;

        public readonly bool IsGeneric;
        // If function is not generic, this is null
        public readonly string[] Typeargs;

        public readonly FunctionType Predicate;

        public FunctionNode(string name, FunctionType predicate, string fileAndLine) : base(fileAndLine)
        {
            Name = name;
            IsExternal = true;
            IsGeneric = false; // external functions are never generic
            Overloads = null;
            Predicate = predicate;
        }

        public FunctionNode(string name, (Pattern[], Node, WhereClauseNode)[] overloads, FunctionType predicate, string[] typeargs, string fileAndLine) : base(fileAndLine)
        {
            Name = name;
            IsExternal = false;
            Overloads = overloads;
            Predicate = predicate;
            if (typeargs is null || typeargs.Length == 0)
                IsGeneric = false;
            else
            {
                IsGeneric = true;
                Typeargs = typeargs;
            }
        }

        public string GetMangledName()
            => Predicate.GetNamedFunctionMangledName(Name);

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitFunction(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitFunction(this);
        }

        public override Node Clone() => throw new Exception(); // function nodes should not be cloned on their own
    }
}
