using System;
using Logger_Core;
using static Logger_Core.Logger;

namespace testing_console
{
    internal static class Program
    {
        public static void Main()
        {
            LogFile("This is a message!", LogLevel.Info);
            LogFile("This is another message!", LogLevel.Error);

            Console.ReadKey();
        }
    }
}