using System;
using System.Linq;

namespace SingleOrDefault
{
    class Program
    {
        static void Main(string[] args)
        {
            //Returns a single, specific element of a sequence, or a default value if that element is not found.

            int[] array = { 1, 2, 3 };

            // Default is returned if no element found(default value of int is 0)
            int a = array.SingleOrDefault(element => element == 5);
            Console.WriteLine(a);

            // Value is returned if one is found
            int b = array.SingleOrDefault(element => element == 1);
            Console.WriteLine(b);

            try
            {
                // Exception is thrown if more than one is found
                int c = array.SingleOrDefault(element => element >= 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
            }
        }
    }
}
