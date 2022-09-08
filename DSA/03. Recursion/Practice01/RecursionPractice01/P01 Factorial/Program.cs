using System;

namespace P01_Factorial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());

            Console.WriteLine(Factorial(input));
        }

        static long Factorial(int input)
        {
            if (input==1)
            {
                return input;
            }
            return input * Factorial(input - 1);
        }
    }
}
