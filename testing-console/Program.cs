using Logger_Core;
using static Logger_Core.Logger;

namespace testing_console
{
    internal static class Program
    {
        public static void Main()
        {
            Log("This is an info message!", LogLevel.Info);
            Log("This is a warning message!", LogLevel.Warning);
            Log("This is an error message!", LogLevel.Error);
            Log("This is a fatal message!", LogLevel.Fatal);
            Log("This is a messages indicating a succesful action has been performed!", LogLevel.Succes);
        }
    }
}