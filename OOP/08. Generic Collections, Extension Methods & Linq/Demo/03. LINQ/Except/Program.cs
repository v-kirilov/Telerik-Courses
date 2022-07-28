using System;
using System.Linq;

namespace Except
{
    class Program
    {
        static void Main(string[] args)
        {
            //Produces the set difference of two sequences.

            // Contains four values.
            int[] values1 = { 1, 2, 3, 4, 5, 6 };

            // Contains three values (1 and 2 also found in values1).
            int[] values2 = { 1, 2, 7 };

            // Remove all values2 from values1.
            var result = values1.Except(values2);

            // Print result
            foreach (var element in result)
            {
                Console.WriteLine(element);
            }
        }
    }
}
