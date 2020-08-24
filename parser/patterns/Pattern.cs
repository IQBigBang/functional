using System;
using System.Collections.Generic;
using Functional.ast;
using Functional.types;

namespace Functional.parser.patterns
{
    public interface Pattern : IDeepCloneable<Pattern>
    {
        /// <summary>
        /// Compile the pattern.
        /// The compiled piece of code is an expression
        /// that returns true if the arguments hold up to the pattern
        /// </summary>
        /// <param name="baseName">The name that can be used to access the value the pattern matches against</param>
        /// <returns>The test expression</returns>
        string CompileTest(string baseName);

        /// <summary>
        /// Checks whether the pattern is a valid option
        /// for the type supplied
        /// </summary>
        /// <returns><c>true</c>, if type was matched, <c>false</c> otherwise.</returns>
        bool MatchesType(Ty type);

        /// <summary>
        /// Sets the internal type of the pattern (if the pattern requires its type)
        /// The type is expected to be a valid option for this pattern (it should be checked by MatchesType first)
        /// </summary>
        /// <param name="type">The type of the pattern</param>
        void SetType(Ty type);

        /// <summary>
        /// Gets all the binded arguments from the pattern (and its subpatterns)
        /// </summary>
        /// <returns>The bindings in form (bindName, bindType)</returns>
        void GetBindingsTypes(ref Dictionary<string, Ty> bindings);

        /// <summary>
        /// Gets all the binded arguments from the pattern (and its subpatterns)
        /// </summary>
        /// <returns>The bindings in form (bindName, cCode)</returns>
        /// <param name="baseName">The name that can be used to access the value the pattern matches against</param>
        void GetBindings(ref Dictionary<string, string> bindings, string baseName);
    }
}

