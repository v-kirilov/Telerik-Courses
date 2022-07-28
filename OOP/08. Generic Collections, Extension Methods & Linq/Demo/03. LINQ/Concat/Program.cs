using System;
using System.Linq;

namespace Concat
{
    class Program
    {
        static void Main(string[] args)
        {
            //Concatenates two sequences.

            int[] array1 = { 1, 2, 3 };
            int[] array2 = { 4, 5, 6 };

            // Concat array1 and array2
            var result1 = array1.Concat(array2);

            foreach (int value in result1)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine();

            // Concat array2 and then array1 
            var result2 = array2.Concat(array1);

            foreach (int value in result2)
            {
                Console.WriteLine(value);
            }
        }
    }
}
