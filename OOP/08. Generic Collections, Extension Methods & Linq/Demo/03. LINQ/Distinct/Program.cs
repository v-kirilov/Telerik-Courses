using System;
using System.Linq;

namespace Distinct
{
    class Program
    {
        static void Main(string[] args)
        {
            //Returns distinct elements from a sequence.

            int[] array1 = { 1, 2, 2, 3, 4, 4, 5 };

            var result = array1.Distinct();

            // Distinct returns unique values
            foreach (int value in result)
            {
                Console.WriteLine(value);
            }
        }
    }
}
