using System;
using System.Linq;

namespace First
{
    class Program
    {
        static void Main(string[] args)
        {
            //Returns the first element of a sequence.

            int[] values = { 3, 4, 5, 4};

            // returns the first object
            int result = values.First();

            // returns the first matching object
            int result2 = values.First(x => x == 4);
             
            Console.WriteLine(result);

            Console.WriteLine(result2);
        }
    }
}
