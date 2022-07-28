using System;
using System.Linq;

namespace Skip
{
    class Program
    {
        static void Main(string[] args)
        {
            //Bypasses a specified number of elements in a sequence and then returns the remaining elements.

            int[] array = { 1, 2, 3, 4, 5, 6 };

            var items1 = array.Skip(2);
            foreach (var value in items1)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine();

            var items2 = array.Skip(4);
            foreach (var value in items2)
            {
                Console.WriteLine(value);
            }
        }
    }
}
