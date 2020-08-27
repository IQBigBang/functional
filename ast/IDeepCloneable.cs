using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Functional.types;

namespace Functional.ast
{
    public interface IDeepCloneable<out T>
    {
        /// <summary>
        /// Clones the abstract syntax tree into a new one.
        /// </summary>
        /// <param name="newTypes">This is used to replace type-hints</param>
        /// <returns></returns>
        public T Clone(Dictionary<string, Ty> newTypes);
    }

    public static class MapExtensions
    {
        public static S[] Map<T, S>(this T[] arr, Func<T, S> map) => arr.Select(map).ToArray();
    }
}
