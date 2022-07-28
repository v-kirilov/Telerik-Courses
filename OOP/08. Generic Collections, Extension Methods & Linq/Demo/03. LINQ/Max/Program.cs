using System;
using System.Linq;

namespace Max
{
    class Program
    {
        static void Main(string[] args)
        {
            //Returns the maximum value in a sequence of values.

            int[] array1 = { -3, 1, 2, 3, 4, -2, -6};

            // Find maximum number.
            Console.WriteLine(array1.Max());

            // Find maximum number when all numbers are made positive.
            Console.WriteLine(array1.Max(element => Math.Abs(element)));
        }
    }
}
