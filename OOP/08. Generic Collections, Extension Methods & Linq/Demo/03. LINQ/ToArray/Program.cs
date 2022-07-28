using System;
using System.Collections.Generic;

namespace ToArray
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creates an array from a IEnumerable<T>.

            List<string> input = new List<string>{ "John", "David", "Michael", "Bryan", "Catherine", "Lilly" };

            string[] output = input.GetRange(2, 3).ToArray();

            foreach (string name in output)
            {
                Console.WriteLine(name);
            }
        }
    }
}
