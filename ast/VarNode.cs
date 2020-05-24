using System;
using System.Linq;
using Functional.types;

namespace Functional.ast
{
    public class VarNode : Node
    {
        /// <summary>
        /// The name of the variable.
        /// If the variable is a global function, the TypeChecker
        /// changes the name to a mangled name!
        /// </summary>
        public string Name;
        // Warning: TypeHint may be null
        public Ty TypeHint;
        // Because the TypeChecker might alter the type hint
        // it is useful to know whether the user supplied a type hint or not
        public bool UserSuppliedTypeHint;

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
    }
}
