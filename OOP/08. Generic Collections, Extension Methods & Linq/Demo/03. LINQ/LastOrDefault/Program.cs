using System;
using System.Linq;

namespace LastOrDefault
{
    class Program
    {
        static void Main(string[] args)
        {
            //Returns the last element of a sequence, or a default value if no element is found.

            // Last or default.
            int[] array1 = { 1, 2, 3 };
            Console.WriteLine(array1.LastOrDefault());

            // Last when there are no elements.
            int[] array2 = { };
            Console.WriteLine(array2.LastOrDefault());

            // Last odd number.
            Console.WriteLine(array1.LastOrDefault(element => element % 2 != 0));
        }
    }
}
