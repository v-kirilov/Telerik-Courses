using System;
using System.Collections.Generic;
using System.Linq;

namespace Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            //Computes the sum of a sequence of numeric values.

            // Declare a collection of int elements
            int[] array1 = { 1, 2, 3, 4, 5 };

            // Use Sum extension on their elements
            int sum1 = array1.Sum();

            // Print result
            Console.WriteLine(sum1);
        }
    }
}
