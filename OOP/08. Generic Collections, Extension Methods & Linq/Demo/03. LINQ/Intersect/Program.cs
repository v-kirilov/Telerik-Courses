using System;
using System.Linq;

namespace Intersect
{
    class Program
    {
        static void Main(string[] args)
        {
            //Produces the set intersection of two sequences.

            // Assign two arrays.
            int[] array1 = { 1, 2, 3 };
            int[] array2 = { 2, 3, 4 };

            // Call Intersect extension method.
            var intersect = array1.Intersect(array2);

            // Print intersect result.
            foreach (int value in intersect)
            {
                Console.WriteLine(value);
            }
        }
    }
}
