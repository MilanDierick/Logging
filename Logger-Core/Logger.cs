using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            CheckForFile(true, false);
        }

        /// <summary>
        /// Log prints a colored message to the console window.
        /// </summary>
        /// <param name="message">Contains the message that will be displayed.</param>
        /// <param name="logLevel">Indicates the severity of the message.</param>
        /// <param name="omitStamps"></param>
        // ReSharper disable once MemberCanBePrivate.Global
        public static void Log(string message, LogLevel logLevel, bool omitStamps = false)
        {
            SetConsoleColour(logLevel);

            if (!omitStamps)
                Console.WriteLine("[" + DateStamp() + " | " + TimeStamp() + "] " + logLevel.ToString().ToUpper() +
                                  ": " + message);
            else
                Console.WriteLine(message);
        }

        /// <summary>
        /// LogFile writes a formatted message to the log file.
        /// </summary>
        /// <param name="message">Contains the message that will be written to the file.</param>
        /// <param name="logLevel">Indicates the severity of the message.</param>
        /// <param name="logToConsole"></param>
        /// <param name="omitStamps"></param>
        // ReSharper disable once MemberCanBePrivate.Global
        public static void LogFile(string message, LogLevel logLevel, bool logToConsole = true, bool omitStamps = false)
        {
            if (logToConsole)
            {
                Log(message, logLevel, omitStamps);
            }

            using (var streamWriter = new StreamWriter(FilePath + FileName, true))
            {
                if (!omitStamps)
                    streamWriter.WriteLine("[" + DateStamp() + " | " + TimeStamp() + "] " +
                                           logLevel.ToString().ToUpper() + ": " + message);
                else
                    streamWriter.WriteLine(message);
            }
        }

        /// <summary>
        /// Helper method to read all the errors that have been written to the log file.
        /// </summary>
        /// <returns>A List that contains all the errors that have been written to the log file.</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public static IEnumerable<string> LoadLog()
        {
            return new List<string>(File.ReadAllLines(FilePath + FileName).ToList());
        }

        /// <summary>
        /// Prints all the entrys in the log file to the console.
        /// </summary>
        public static void PrintLoadedLog()
        {
            foreach (var entry in LoadLog())
            {
                switch (entry[29])
                {
                    case 'I':
                        Log(entry, LogLevel.Info, true);
                        break;
                    case 'W':
                        Log(entry, LogLevel.Warning, true);
                        break;
                    case 'E':
                        Log(entry, LogLevel.Error, true);
                        break;
                    case 'F':
                        Log(entry, LogLevel.Fatal, true);
                        break;
                    case 'S':
                        Log(entry, LogLevel.Succes, true);
                        break;
                    default:
                        LogFile("Error while processing entry from loaded log: invalid entry [" + entry[29] + "]!",
                            LogLevel.Fatal);
                        break;
                }
            }
        }

        /// <summary>
        /// Sets the desired console text colour.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <exception cref="ArgumentOutOfRangeException">Exception that is thrown when the provided loglevel is not valid.</exception>
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

        /// <summary>
        /// Helper method to generate a formatted string containing the current time.
        /// </summary>
        /// <returns>Returns a formatted string containing the current time.</returns>
        private static string TimeStamp() => DateTime.Now.ToString("HH:mm:ss tt zz");

        /// <summary>
        /// Helper method to generate a formatted string containing the current date.
        /// </summary>
        /// <returns>Returns a formatted string containing the current date.</returns>
        private static string DateStamp() => DateTime.Today.ToShortDateString();

        /// <summary>
        /// Helper method that checks if a file exists, and, if required, creates a new file.
        /// </summary>
        /// <param name="createMissingFile">Indicates if a new file should be created when the required file doesn't exist.</param>
        /// <param name="force">Indicates if a new file should be created,even if the file already exists.</param>
        /// <returns>Returns a boolean that indicates if the file exists.</returns>
        private static bool CheckForFile(bool createMissingFile, bool force)
        {
            if (force)
                File.Create(FilePath + FileName);
            else if (!File.Exists(FilePath + FileName))
                if (createMissingFile)
                    File.Create(FilePath + FileName);
                else
                {
                    Log("Log file doesn't exist, and the user doesn't want to create it.", LogLevel.Warning);
                    return false;
                }

            LogFile("This file exists!", LogLevel.Succes);
            return true;
        }
    }
}