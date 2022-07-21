using System;

namespace ExceptionHandling
{
    class ExceptionHandling
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine(); // Read input from the console

            try // Try to parse the input to an integer
            {
                int.Parse(input);
                Console.WriteLine("You entered valid Int32 number {0}.", input);
            }
            catch (FormatException) // Catch the exception if input can't be parsed to int
            {
                Console.WriteLine("Invalid integer number!");
            }
            catch (OverflowException) // Catch the exception if parsing results in an overflow
            {
                Console.WriteLine("The number is too big to fit in Int32!");
            }
        }
    }
}
