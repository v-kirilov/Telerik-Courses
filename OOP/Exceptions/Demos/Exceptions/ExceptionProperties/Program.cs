using System;

namespace ExceptionProperties
{
    class ExceptionProperties
    {
        static void Main()
        {
            try
            {
                CauseFormatException();
            }
            catch (FormatException e)
            {
                Console.Error.WriteLine("Exception caught: {0}");
                Console.Error.WriteLine("\r\nMessage: {0}", e.Message);
                Console.Error.WriteLine("\r\nStack Trace: {0}", e.StackTrace);
            }
        }

        public static void CauseFormatException()
        {
            string input = "an invalid number";
            int.Parse(input);
        }
    }
}
