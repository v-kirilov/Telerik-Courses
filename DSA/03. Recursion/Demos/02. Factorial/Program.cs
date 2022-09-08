using System;

namespace Factorial
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(Factorial(10));
        }

        static int Factorial(int n)
        {
            // Bottom case
            if (n <= 1)
            {
                return 1;
            }

            // If n is 5 => return 5 & Factorial(4)
            return n * Factorial(n - 1);
        }
    }
}
