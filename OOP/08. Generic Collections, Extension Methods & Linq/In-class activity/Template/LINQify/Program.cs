using System;
using System.Collections.Generic;
using LINQify.Tasks;

namespace LINQify
{
    class Program
    {
        static void Main(string[] args)
        {
            var people = Helper.GetData();

            //You can test your implementations here:

           var result1 = Task12.Execute(people);
           var result2 = Task12.ExecuteWithLINQ(people);
          

            Console.WriteLine();
        }
    }
}