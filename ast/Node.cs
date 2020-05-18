using System;
using System.Collections.Generic;
using Functional.debug;
using Functional.types;

namespace Functional.ast
{
    public abstract class Node
    {
        protected Node(string fileAndLine)
        {
            FileAndLine = fileAndLine;
        }

        public AstType NodeType;
        // file:line - used in error reporting
        public readonly string FileAndLine;
        public abstract void Accept(AstVisitor visitor);
        public abstract T Accept<T>(AstVisitor<T> visitor);

        public virtual void ResolveNamedTypes(Dictionary<string, AstType> aliases)
        {
            NodeType.ResolveNamedTypes(aliases);
        }

        public void ReportError(string format, params object[] args)
            => ErrorReporter.Error(format, this.FileAndLine, args);

        public void ReportWarning(string format, params object[] args)
            => ErrorReporter.Warning(format, this.FileAndLine, args);

        public void ReportNote(string format, params object[] args)
            => ErrorReporter.Note(format, this.FileAndLine, args);
    }
}
