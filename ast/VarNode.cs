using System;
using System.Collections.Generic;
using System.Linq;
using Functional.types;

namespace Functional.ast
{
    public class VarNode : Node
    {
        /// <summary>
        /// The name of the variable.
        ///
        /// Name is not readonly (like other fields), because the type-checker might change it.
        /// Therefore be careful when using this property;
        /// </summary>
        public string Name;
        /// Warning: TypeHint may be null
        /// TypeHint is not readonly (like other fields), because the type-checker might insert a type-hint.
        /// Therefore be careful when using this property
        public Ty TypeHint;
        // Because the TypeChecker might alter the type hint
        // it is useful to know whether the user supplied a type hint or not
        public readonly bool UserSuppliedTypeHint;

        public VarNode(string name, Ty typeHint, string fileAndLine) : base(fileAndLine)
        {
            Name = name;
            TypeHint = typeHint;
            UserSuppliedTypeHint = !(typeHint is null);
        }

        public override void Accept(AstVisitor visitor)
        {
            visitor.VisitVar(this);
        }

        public override T Accept<T>(AstVisitor<T> visitor)
        {
            return visitor.VisitVar(this);
        }

        public override Node Clone(Dictionary<string, Ty> newTypes) => new VarNode(Name, TypeHint?.Monomorphize(newTypes), FileAndLine);
    }
}
