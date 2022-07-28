using System;
using System.Linq;

namespace SequenceEquals
{
    class Program
    {
        static void Main(string[] args)
        {
            //Determines whether two sequences are equal according to an equality comparer.

            string[] array1 = { "Telerik", "Academy", ".Net" };
            string[] array2 = { "something", "else", "" };
            string[] array3 = { "Telerik", "Academy", ".Net" };
            string[] array4 = { "TELERIK", "ACADEMY", ".NET" };

            bool a = array1.SequenceEqual(array2);
            bool b = array1.SequenceEqual(array3);
            bool c = array1.SequenceEqual(array4, StringComparer.InvariantCultureIgnoreCase);

            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
        }
    }
}
