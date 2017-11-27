using System;
using Logger_Core;
using static Logger_Core.Logger;

namespace testing_console
{
    internal static class Program
    {
        public static void Main()
        {
            LogFile("This is an info message", LogLevel.Info);
            LogFile("This is a warning message", LogLevel.Warning);
            LogFile("This is an error message", LogLevel.Error);
            LogFile("This is a fatal message", LogLevel.Fatal);
            LogFile("This is a succes message", LogLevel.Succes);
            
            PrintLoadedLog();
                        
            Console.ReadKey();
        }
    }
}