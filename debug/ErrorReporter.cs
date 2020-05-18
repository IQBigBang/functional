using System;
using System.Drawing;
using System.Threading;
using Functional.ast;
using Pastel;

namespace Functional.debug
{
    public static class ErrorReporter
    {
        public static string Bold(this string s)
            => "\u001b[1m" + s + "\u001b[0m";

        public static bool ThrowExceptions = false;

        public static void Error(string format, string FileAndLine, params object[] args)
        {
            Console.WriteLine(string.Format("{0}: {1}: {2}",
                FileAndLine, "error".Pastel(Color.Red), string.Format(format, args).Bold()).Bold());
            if (ThrowExceptions) throw new Exception();
            Environment.Exit(1);
        }

        public static void Warning(string format, string FileAndLine, params object[] args)
        {
            Console.WriteLine(string.Format("{0}: {1}: {2}",
                FileAndLine, "warning".Pastel(Color.Yellow), string.Format(format, args).Bold()).Bold());
        }

        public static void Note(string format, string FileAndLine, params object[] args)
        {
            Console.WriteLine(string.Format("{0}: {1}: {2}",
                FileAndLine, "note".Pastel(Color.SkyBlue), string.Format(format, args).Bold()).Bold());
        }

        public static void Error(string format, params object[] args)
        {
            Console.WriteLine(string.Format("{0}: {1}",
                "error".Pastel(Color.Red), string.Format(format, args).Bold()).Bold());
            if (ThrowExceptions) throw new Exception();
            Environment.Exit(1);
        }

        public static void Warning(string format, params object[] args)
        {
            Console.WriteLine(string.Format("{0}: {1}",
                "warning".Pastel(Color.Yellow), string.Format(format, args).Bold()).Bold());
        }

        public static void Note(string format, params object[] args)
        {
            Console.WriteLine(string.Format("{0}: {1}",
                "note".Pastel(Color.SkyBlue), string.Format(format, args).Bold()).Bold());
        }
    }
}
