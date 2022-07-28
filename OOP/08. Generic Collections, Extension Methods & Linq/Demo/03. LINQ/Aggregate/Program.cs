using System;
using System.Linq;

namespace Aggregate
{
    class Program
    {
        static void Main(string[] args)
        {
            //Applies an accumulator function over a sequence. 
            //The specified seed value is used as the initial accumulator value, and the specified function is used to select the result value.

            int[] array = { 1, 2, 3, 4, 5, 6 };
            int result = array.Aggregate((a, b) => a + b);

            // 1 + 2 = 3
            // 3 + 3 = 6
            // 6 + 4 = 10
            // 10 + 5 == 15

            Console.WriteLine(result);
        }
    }
}
