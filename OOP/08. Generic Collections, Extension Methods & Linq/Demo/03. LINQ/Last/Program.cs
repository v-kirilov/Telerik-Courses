using System;
using System.Linq;

namespace Last
{
    class Program
    {
        static void Main(string[] args)
        {
            //Returns the last element of a sequence.

            int[] values = { 1, 2, 3, 4, 5, 6 };

            int last = values.Last();
            int lastOdd = values.Last(element => element % 2 != 0);

            Console.WriteLine(last);
            Console.WriteLine(lastOdd);
        }
    }
}
