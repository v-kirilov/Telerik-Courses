using System;
using System.Collections.Generic;
using System.Linq;

namespace ToList
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creates a List<T> from an IEnumerable<T>.

            List<string> input = new List<string> { "John", "David", "Michael", "Bryan", "Catherine", "Lilly" };

            List<string> list = input.ToList();

            foreach (string value in list)
            {
                Console.WriteLine(value);
            }
        }
    }
}
