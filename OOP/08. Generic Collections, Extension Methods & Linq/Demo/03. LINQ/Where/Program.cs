using System;
using System.Linq;

namespace Where
{
    class Program
    {
        static void Main(string[] args)
        {
            //Filters a sequence of values based on a predicate.

            int[] values = { 1, 3, 5, 7, 4, 6, 8, 10 };

            // take elements where condition is met(in this case when value is odd)
            var result = values.Where(item => item % 2 != 0);


            foreach (int item in result)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            var result2 = values.Where(item => item > 9);

            foreach (int item in result2)
            {
                Console.WriteLine(item);
            }
        }
    }
}
