using System;
using System.Runtime.InteropServices;

namespace Functional.debug
{
    public static class ErrorReporter
    {
        public static bool ThrowExceptions = false;
        public static bool UseColors = true;

        private static string AnsiCode(string code)
        {
            if (!UseColors) return "";
            return "\x1B[" + code + "m";
        }
        private static string Reset { get { return AnsiCode("0"); } }
        private static string Bold { get { return AnsiCode("1"); } }
        private static string Red { get { return AnsiCode("31"); } }
        private static string Yellow { get { return AnsiCode("33"); } }
        private static string Cyan { get { return AnsiCode("36"); } }

        // These handles are used only on Windows
        // Fortunately they don't prevent compilation on Linux

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        /// <summary>
        /// On Windows, this enables colors in the output
        /// On other platforms, it's a noop
        /// </summary>
        public static void Init()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var OutHandle = GetStdHandle(-11); // -11 = StdOutputHandle
                if (OutHandle == (IntPtr)(-1))
                    UseColors = false;
                if (!GetConsoleMode(OutHandle, out uint OriginalMode))
                    UseColors = false;
                uint OutMode = OriginalMode | 4; // 4 = Enable virtual colors
                if (!SetConsoleMode(OutHandle, OutMode))
                    UseColors = false;
            }
        }

        // FL = File and Line
        public static void ErrorFL(string format, string FileAndLine, params object[] args)
        {
            Console.WriteLine(
                Bold + FileAndLine + ": " 
                + Red + "error" + Reset + Bold + ": " 
                + string.Format(format, args) + Reset);
            if (ThrowExceptions) throw new Exception();
            //Environment.Exit(1);
        }

        public static void WarningFL(string format, string FileAndLine, params object[] args)
        {
            Console.WriteLine(
                Bold + FileAndLine + ": "
                + Yellow + "warning" + Reset + Bold + ": "
                + string.Format(format, args) + Reset);
        }

        public static void NoteFL(string format, string FileAndLine, params object[] args)
        {
            Console.WriteLine(
                Bold + FileAndLine + ": "
                + Cyan + "note" + Reset + Bold + ": "
                + string.Format(format, args) + Reset);
        }

        public static void Error(string format, params object[] args)
        {
            Console.WriteLine(
                Bold + Red + "error" + Reset + Bold + ": "
                + string.Format(format, args) + Reset);
            if (ThrowExceptions) throw new Exception();
            //Environment.Exit(1);
        }

        public static void Warning(string format, params object[] args)
        {
            Console.WriteLine(
                Bold + Yellow + "warning" + Reset + Bold + ": "
                + string.Format(format, args) + Reset);
        }

        public static void Note(string format, params object[] args)
        {
            Console.WriteLine(
                Bold + Cyan + "note" + Reset + Bold + ": "
                + string.Format(format, args) + Reset);
        }
    }
}
