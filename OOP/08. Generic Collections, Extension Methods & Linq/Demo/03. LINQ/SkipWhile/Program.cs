using System;
using System.Linq;

namespace SkipWhile
{
    class Program
    {
        static void Main(string[] args)
        {
            //Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements.

            int[] array = { 1, 3, 5, 10, 20 };

            var result = array.SkipWhile(element => element < 10);

            foreach (int value in result)
            {
                Console.WriteLine(value);
            }
        }
    }
}
