﻿using System;
using System.Collections.Generic;
using Functional.types;

namespace Functional.parser.patterns
{
    public interface Pattern
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
        bool MatchesType(AstType type);

        /// <summary>
        /// Gets all the binded arguments from the pattern (and its subpatterns)
        /// </summary>
        /// <returns>The bindings in form (bindName, bindType)</returns>
        void GetBindingsTypes(ref Dictionary<string, AstType> bindings);

        /// <summary>
        /// Gets all the binded arguments from the pattern (and its subpatterns)
        /// </summary>
        /// <returns>The bindings in form (bindName, cCode)</returns>
        /// <param name="baseName">The name that can be used to access the value the pattern matches against</param>
        void GetBindings(ref Dictionary<string, string> bindings, string baseName);
    }
}

