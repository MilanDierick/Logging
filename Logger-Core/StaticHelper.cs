using System;
using System.IO;

namespace Logger_Core
{
    public static class StaticHelper
    {
        /// <summary>
        /// Helper method that sets the desired console text colour.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <exception cref="NotImplementedException">Thrown when the requesting log level is not implemented.</exception>
        internal static void SetConsoleColour(LogLevel logLevel)
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
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }
        
        /// <summary>
        /// Helper method to generate a formatted string containing the current time.
        /// </summary>
        /// <returns>Returns a formatted string containing the current time.</returns>
        internal static string TimeStamp() => DateTime.Now.ToString("HH:mm:ss tt zz");

        /// <summary>
        /// Helper method to generate a formatted string containing the current date.
        /// </summary>
        /// <returns>Returns a formatted string containing the current date.</returns>
        internal static string DateStamp() => DateTime.Today.ToShortDateString();

        /// <summary>
        /// Helper method that checks if a file exists, and, if required, creates a new file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="createMissingFile">Indicates if a new file should be created when the required file doesn't exist.</param>
        /// <param name="force">Indicates if a new file should be created,even if the file already exists.</param>
        /// <returns>Returns a boolean that indicates if the file exists.</returns>
        internal static void CheckForFile(string filePath, string fileName, bool createMissingFile, bool force)
        {
            if (force)
                File.Create(filePath + fileName);
            else if (!File.Exists(filePath + fileName))
                if (createMissingFile)
                    File.Create(filePath + fileName);
        }
    }
}