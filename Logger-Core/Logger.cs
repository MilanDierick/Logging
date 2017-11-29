using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using static Logger_Core.StaticHelper;

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
            CheckForFile(FilePath, FileName, true, false);
        }

        /// <summary>
        /// Log prints a colored message to the console window.
        /// </summary>
        /// <param name="message">Contains the message that will be displayed.</param>
        /// <param name="logLevel">Indicates the severity of the message.</param>
        /// <param name="omitStamps"></param>
        [PublicAPI]
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
        [PublicAPI]
        public static void LogFile(string message, LogLevel logLevel, bool logToConsole = true, bool omitStamps = false)
        {
            if (logToConsole)
            {
                Log(message, logLevel, omitStamps);
            }

            try
            {
                using (var streamWriter = new StreamWriter(FilePath + FileName, true))
                {
                    if (!omitStamps)
                        streamWriter.WriteLine("[" + DateStamp() + " | " + TimeStamp() + "] " +
                                               logLevel.ToString().ToUpper() + ": " + message);
                    else
                        streamWriter.WriteLine(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
                Log("message: '" + message + "' has not been logged to the log file!", LogLevel.Warning);
            }
        }

        /// <summary>
        /// Helper method to read all the errors that have been written to the log file.
        /// </summary>
        /// <returns>A List that contains all the errors that have been written to the log file.</returns>
        [PublicAPI]
        [MustUseReturnValue]
        public static IEnumerable<string> LoadLog()
        {
            return new List<string>(File.ReadAllLines(FilePath + FileName).ToList());
        }

        /// <summary>
        /// Prints all the entrys in the log file to the console.
        /// </summary>
        [PublicAPI]
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
    }
}