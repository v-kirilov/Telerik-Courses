using System;
using System.Linq;

namespace TakeWhile
{
    class Program
    {
        static void Main(string[] args)
        {
            //Returns elements from a sequence as long as a specified condition is true, and then skips the remaining elements.

            int[] values = { 1, 3, 5, 7, 4, 6, 8, 10 };

            // take elements while condition is met(in this case when value is odd)
            var result = values.TakeWhile(item => item % 2 != 0);

            foreach (int value in result)
            {
                Console.WriteLine(value);
            }
        }
    }
}
