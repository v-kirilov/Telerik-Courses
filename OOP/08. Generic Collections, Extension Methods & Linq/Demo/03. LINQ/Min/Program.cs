using System;
using System.Linq;

namespace Min
{
    class Program
    {
        static void Main(string[] args)
        {
            //Returns the minimum value in a sequence of values.

            int[] array1 = { -3, 1, 2, 3, 4, 5, -2, };

            // Find minimum number.
            Console.WriteLine(array1.Min());

            // Find minimum number when signs are swapped
            Console.WriteLine(array1.Min(element => -element));
        }
    }
}
