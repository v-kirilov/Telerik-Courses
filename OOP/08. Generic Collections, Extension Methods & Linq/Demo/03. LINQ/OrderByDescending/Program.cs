using System;
using System.Linq;

namespace OrderByDescending
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sorts the elements of a sequence in descending order.

            string[] names = { "John", "David", "Anthony", "Michael" };

            var result = names.OrderByDescending(x => x);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
