using System;
using System.Collections.Generic;
using System.Linq;

namespace DefaultIfEmpty
{
    class Program
    {
        static void Main(string[] args)
        {
            // Empty list.
            List<int> list = new List<int>();
            var result = list.DefaultIfEmpty();

            // One element in collection with default(int) value.
            foreach (var value in result)
            {
                Console.WriteLine(value);
            }

            result = list.DefaultIfEmpty(7);

            // One element in collection with 7 value.
            foreach (var value in result)
            {
                Console.WriteLine(value);
            }
        }
    }
}
