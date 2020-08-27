using System;
using System.Collections.Generic;
using System.Linq;
using Functional.parser.patterns;
using Functional.types;

namespace Functional.ast
{
    public class WhereClauseNode : Node
    {
        public readonly (Pattern, Node)[] Bindings;

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

        public override Node Clone(Dictionary<string, Ty> newTypes) => new WhereClauseNode(
            Bindings.Map(x => (x.Item1.Clone(newTypes), x.Item2.Clone(newTypes))),
            FileAndLine);
    }
}
