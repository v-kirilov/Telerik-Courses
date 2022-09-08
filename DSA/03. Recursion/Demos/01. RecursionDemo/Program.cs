using System;
using System.Collections.Generic;

namespace RecursionDemo
{
    class Program
    {
        static void Main()
        {
            PrintNumber(10);
        }

        static void PrintNumber(int n)
        {
            // Bottom case
            if (n == 0)
            {
                return;
            }

            // TODO: Move this line after PrintNumber and examine the output!
            Console.WriteLine(n);

            PrintNumber(n - 1);
        }
    }
}
