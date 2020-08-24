using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Functional.ast
{
    public interface IDeepCloneable<out T>
    {
        public T Clone();
    }

    public static class ArrayCloneExtension
    {
        public static T[] DeepClone<T>(this T[] arr) where T : IDeepCloneable<T> => arr.Select(x => x.Clone()).ToArray();
        public static (T0, T1) Clone<T0, T1>(this (T0, T1) tuple)
            where T0 : IDeepCloneable<T0>
            where T1 : IDeepCloneable<T1>
            => (tuple.Item1.Clone(), tuple.Item2.Clone());

        public static (T0, T1, T2) Clone<T0, T1, T2>(this (T0, T1, T2) tuple)
            where T0 : IDeepCloneable<T0>
            where T1 : IDeepCloneable<T1>
            where T2: IDeepCloneable<T2>
            => (tuple.Item1.Clone(), tuple.Item2.Clone(), tuple.Item3.Clone());
    }
}
