using System;

namespace Logger_Core
{
    /// <summary>
    /// Logger is the class that contains methods to print colored messages in the console window or save messages in a file.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Log is used to print a colored message to the console window.
        /// </summary>
        /// <param name="message">Contains the message that will be displayed.</param>
        /// <param name="logLevel">Indicates the severity of the message.</param>
        public static void Log(string message, LogLevel logLevel)
        {
            SetConsoleColour(logLevel);
            Console.WriteLine("[" + TimeStamp() + "] " + logLevel.ToString().ToUpper() + ": " + message);
        }

        private static void SetConsoleColour(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Fatal:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogLevel.Succes:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        private static string TimeStamp()
        {
            return DateTime.Now.ToString("HH:mm:ss tt zz");
        }
    }
}