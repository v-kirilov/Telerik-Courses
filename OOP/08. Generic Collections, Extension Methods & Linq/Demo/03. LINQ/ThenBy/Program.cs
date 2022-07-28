using System;
using System.Linq;

namespace ThenBy
{
    class Program
    {
        static void Main(string[] args)
        {
            //Performs a subsequent ordering of the elements in a sequence in ascending order.

            string[] names = { "John", "David", "Anthony", "Michael" };

            var result = names
                .OrderBy(name => name.Length)
                .ThenBy(name => name);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
