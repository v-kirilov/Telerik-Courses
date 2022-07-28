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
            //Your implementation goes here
        }

        // "This sample uses First to find the first element in the array that starts with 'o'.
        public static void Example02()
        {
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            //Your implementation goes here
        }

        // This sample uses FirstOrDefault to try to return the first element of the sequence
        // unless there are no elements, in which case the default value for that type is returned. 
        public static void Example03()
        {
            int[] numbers = { };
            //Your implementation goes here
        }

        // This sample uses FirstOrDefault to return the first product whose ProductID is 789 as a single Product object, unless there is no match, in which case null is returned.
        public static void Example04()
        {
            var products = Database.Products;
            //Your implementation goes here
        }

        // This sample uses ElementAt to retrieve the second number greater than 5 from an array.
        public static void Example05()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
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
