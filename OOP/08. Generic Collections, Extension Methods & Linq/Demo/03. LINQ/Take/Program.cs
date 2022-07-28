using System;
using System.Linq;

namespace Take
{
    class Program
    {
        static void Main(string[] args)
        {
            //Returns a specified number of contiguous elements from the start of a sequence.

            int[] array1 = { 1, 2, 3, 4, 5 };

            // Get first 2 elements.
            var result = array1.Take(2);

            foreach (int item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
