using Linq.Examples.Data;
using Linq.Examples.Models;
using System;
using System.Linq;
namespace Linq.Examples
{
    public class ElementOperators
    {
        // "This sample uses First to return the first matching element as a Product, instead of as a sequence containing a Product.
        public static void Example01()
        {
            var products = Database.Products;
            Product product12 = (products.Where(prod => prod.ProductID == 12)).First();

            Console.WriteLine(product12.ProductName);
        }

        // "This sample uses First to find the first element in the array that starts with 'o'.
        public static void Example02()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string startsWithO = strings.First(s => s[0] == 'o');
            Console.WriteLine("A string starting with 'o': {0}", startsWithO);
        }

        // This sample uses FirstOrDefault to try to return the first element of the sequence
        // unless there are no elements, in which case the default value for that type is returned. 
        public static void Example03()
        {
            int[] numbers = { };
            int firstNumOrDefault = numbers.FirstOrDefault();
            Console.WriteLine(firstNumOrDefault);
        }

        // This sample uses FirstOrDefault to return the first product whose ProductID is 789 as a single Product object, unless there is no match, in which case null is returned.
        public static void Example04()
        {
            var products = Database.Products;
            Product product789 = products.FirstOrDefault(p => p.ProductID == 789);
            Console.WriteLine("Product 789 exists: {0}", product789 != null);
        }

        // This sample uses ElementAt to retrieve the second number greater than 5 from an array.
        public static void Example05()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int fourthLowNum = (numbers.Where(num => num > 5)).ElementAt(1); // second number is index 1 because sequences use 0-based indexing
            Console.WriteLine("Second number > 5: {0}", fourthLowNum);
        }

        // Run all examples
        public static void RunAllExamples()
        {
            Example01();
            Example02();
            Example03();
            Example04();
            Example05();
        }
    }
}
