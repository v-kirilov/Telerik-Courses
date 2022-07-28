using System;
using System.Collections.Generic;
using System.Linq;

namespace Contains
{
    class Program
    {
        static void Main(string[] args)
        {
            //Determines whether a sequence contains a specified element.

            var list = new List<int>() { 1, 2, 3, 4, 5};

            bool a = list.Contains<int>(3);
            bool b = list.Contains<int>(7);

            Console.WriteLine(a);
            Console.WriteLine(b);
        }
    }
}
