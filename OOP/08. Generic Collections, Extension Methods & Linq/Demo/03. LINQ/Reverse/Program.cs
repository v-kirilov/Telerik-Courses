using System;
using System.Linq;

namespace Reverse
{
    class Program
    {
        static void Main(string[] args)
        {
            //Inverts the order of the elements in a sequence.

            // Create an array.
            int[] array = { 1, 2, 3, 4, 5 };

            // Call reverse extension method on the array.
            var reverse = array.Reverse();

            // Print result of reverse
            foreach (int value in reverse)
            {
                Console.WriteLine(value);
            }
        }
    }
}
