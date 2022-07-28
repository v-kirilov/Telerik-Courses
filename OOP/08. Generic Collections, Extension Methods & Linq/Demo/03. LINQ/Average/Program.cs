using System;
using System.Linq;

namespace Average
{
    class Program
    {
        static void Main(string[] args)
        {
            //Computes the average of a sequence of numeric values.

            double[] array = { 1, 2, 3, 4, 5 };

            double average = array.Average();

            Console.WriteLine(average);
        }
    }
}
