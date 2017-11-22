using System;
using System.IO;

namespace Logger_Core
{
    /// <summary>
    /// Logger is the class that contains methods to print colored messages in the console window or save messages in a file.
    /// </summary>
    public static class Logger
    {
        private static readonly string FilePath = Directory.GetCurrentDirectory();
        private const string FileName = @"\log.txt";

        static Logger()
        {
            if (!CheckForFile(true, false))
                Log("Failed to create log file at path " + FilePath + FileName, LogLevel.Fatal);
            else
                Log("Succesfully created log file at path " + FilePath + FileName, LogLevel.Succes);
        }

        /// <summary>
        /// Log is used to print a colored message to the console window.
        /// </summary>
        /// <param name="message">Contains the message that will be displayed.</param>
        /// <param name="logLevel">Indicates the severity of the message.</param>
        public static void Log(string message, LogLevel logLevel)
        {
            SetConsoleColour(logLevel);
            Console.WriteLine("[" + DateStamp() + " | " + TimeStamp() + "] " + logLevel.ToString().ToUpper() + ": " + message);
        }
        
        /// <summary>
        /// LogFile is used to write a formatted message to a file.
        /// </summary>
        /// <param name="message">Contains the message that will be written to the file.</param>
        /// <param name="logLevel">Indicates the severity of the message.</param>
        /// <param name="logToConsole"></param>
        public static void LogFile(string message, LogLevel logLevel, bool logToConsole = true)
        {
            if (logToConsole)
            {
                Log(message, logLevel);
            }
            
            using (var streamWriter = new StreamWriter(FilePath + FileName, true))
            {
                streamWriter.WriteLine("[" + DateStamp() + " | " + TimeStamp() + "] " + logLevel.ToString().ToUpper() + ": " + message);
            }
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
        
        private static string TimeStamp() => DateTime.Now.ToString("HH:mm:ss tt zz");

        private static string DateStamp() => DateTime.Today.ToShortDateString();

        private static bool CheckForFile(bool createMissingFile, bool force)
        {
            if (File.Exists(FilePath + FileName) && !force) return true;
            if (!createMissingFile) return false;
            File.Create(FilePath + FileName);
            return true;
        }
    }
}