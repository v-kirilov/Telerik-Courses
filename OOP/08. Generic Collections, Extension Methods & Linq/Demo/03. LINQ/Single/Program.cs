using System;
using System.Linq;

namespace Single
{
    class Program
    {
        static void Main(string[] args)
        {
            //Returns a single, specific element of a sequence.

            // Find only element > 500
            int[] array1 = { 1, 3, 600, 4, 5 };
            int value1 = array1.Single(element => element > 500);

            // Ensure only one element
            int[] array2 = { 4 };
            int value2 = array2.Single();

            Console.WriteLine(value1);
            Console.WriteLine(value2);

            // Exception is thrown when more than one element found
            try
            {
                int value3 = array1.Single(element => element > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
            }
        }
    }
}
