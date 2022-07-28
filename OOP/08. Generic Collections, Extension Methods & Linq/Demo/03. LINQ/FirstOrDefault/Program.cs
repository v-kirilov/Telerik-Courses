using System;
using System.Linq;

namespace FirstOrDefault
{
    class Program
    {
        static void Main(string[] args)
        {
            //Returns the first element of a sequence, or a default value if no element is found.

            string[] names = { "Peter", "John", "Robin" };

            // returns the first matching object or default value if not found
            string result = names.FirstOrDefault(x => x == "Peter");

            // No "Michael" found so it returns default value. Default value of string is null
            string result2 = names.FirstOrDefault(x => x == "Michael");

            Console.WriteLine(result);

            Console.WriteLine(result2 == null);
        }
    }
}
