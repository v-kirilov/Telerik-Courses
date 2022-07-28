using System;
using System.Linq;

namespace Any
{
    class Program
    {
        static void Main(string[] args)
        {
            //Determines whether any element of a sequence exists or satisfies a condition.

            int[] array = { 1, 2, 3, 4, 5 };

            // See if any elements are divisible by 2.
            bool b1 = array.Any(item => item % 2 == 0);

            // See if any elements are greater than 3.
            bool b2 = array.Any(item => item > 5);

            // See if any elements are equal to 2.
            bool b3 = array.Any(item => item == 2);

            // Write results.
            Console.WriteLine(b1);
            Console.WriteLine(b2);
            Console.WriteLine(b3);
        }
    }
}
