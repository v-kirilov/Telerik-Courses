using System;
using System.Linq;

namespace OrderBy
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sorts the elements of a sequence in ascending order.

            string[] names = { "John", "David", "Anthony", "Michael" };

            var result = names.OrderBy(x => x);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
