using System;
using System.Linq;

namespace GroupBy
{
    class Program
    {
        static void Main(string[] args)
        {
            //Groups the elements of a sequence.

            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Group elements if they are even.
            var result = array.GroupBy(a => a % 2 == 0);

            // Loop over groups.
            foreach (var group in result)
            {
                // Display key for group.
                Console.WriteLine("IsEven = {0}:", group.Key);

                // Display values in group.
                foreach (var value in group)
                {
                    Console.Write("{0} ", value);
                }

                // End line.
                Console.WriteLine();
            }
        }
    }
}
