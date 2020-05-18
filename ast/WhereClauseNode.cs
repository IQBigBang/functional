using System;
using System.Collections.Generic;
using Functional.parser.patterns;
using Functional.types;

namespace Functional.ast
{
    public class WhereClauseNode : Node
    {
        public (Pattern, Node)[] Bindings { get; }

        public WhereClauseNode((Pattern, Node)[] bindings, string fileAndLine) : base(fileAndLine)
        {
            Bindings = bindings;
        }

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitWhereClause(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitWhereClause(this);
        }
    }
}
