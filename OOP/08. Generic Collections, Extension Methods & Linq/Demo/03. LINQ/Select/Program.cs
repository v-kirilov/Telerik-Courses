using System;
using System.Linq;

namespace Select
{
    class Program
    {
        static void Main(string[] args)
        {
            //Projects each element of a sequence into a new form.

            string[] array = { "cat", "dog", "elepehant", "turtle" };

            // Apply a transformation lambda expression to each element.
            // ... The Select method changes each element in the result.
            var result = array.Select(element => element.ToUpper());

            // Display the result.
            foreach (string value in result)
            {
                Console.WriteLine(value);
            }
        }
    }
}
