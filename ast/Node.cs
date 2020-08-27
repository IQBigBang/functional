using System;
using System.Collections.Generic;
using Functional.debug;
using Functional.types;

namespace Functional.ast
{
    public abstract class Node : IDeepCloneable<Node>
    {
        protected Node(string fileAndLine)
        {
            FileAndLine = fileAndLine;
        }

        public Ty NodeType;

        // file:line - used in error reporting
        public readonly string FileAndLine;
        public abstract void Accept(AstVisitor visitor);
        public abstract T Accept<T>(AstVisitor<T> visitor);
        public abstract Node Clone(Dictionary<string, Ty> newTypes);

        public void ReportError(string format, params object[] args)
            => ErrorReporter.ErrorFL(format, FileAndLine, args);

        public void ReportWarning(string format, params object[] args)
            => ErrorReporter.WarningFL(format, FileAndLine, args);

        public void ReportNote(string format, params object[] args)
            => ErrorReporter.NoteFL(format, FileAndLine, args);
    }
}
