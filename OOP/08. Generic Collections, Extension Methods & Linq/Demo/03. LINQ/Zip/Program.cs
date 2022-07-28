﻿using System;
using System.Linq;

namespace Zip
{
    class Program
    {
        static void Main(string[] args)
        {
            //Zip applies a specified function to the corresponding elements of two sequences, producing a sequence of the results.

            // Two source arrays.
            var array1 = new int[] { 1, 2, 3, 4, 5 };
            var array2 = new int[] { 6, 7, 8, 9, 10 };

            // Add elements at each position together.
            var zip = array1.Zip(array2, (a, b) => (a + b));

            // Look at results.
            foreach (var value in zip)
            {
                Console.WriteLine(value);
            }
        }
    }
}
