using Linq.Examples.Data;
using System;
using System.Linq;

namespace Linq.Examples
{
    public static class RestrictionOperators
    {
        // This sample uses the where clause to find all elements of an array with a value less than 5.
        public static void Example01()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }

        // This sample uses the where clause to find all products that are out of stock.
        public static void Example02()
        {
            var products = Database.Products;
            //Your implementation goes here
        }

        // This sample uses the where clause to find all products that are in stock and cost more than 3.00 per unit.
        public static void Example03()
        {
            var products = Database.Products;
            //Your implementation goes here
        }

        // This sample uses the where clause to find all customers in Washington and then it uses a foreach loop to iterate over the orders collection that belongs to each customer.
        public static void Example04()
        {
            var customers = Database.Customers;
            //Your implementation goes here
        }

        // This sample demonstrates an indexed where clause that returns digits whose name is shorter than their value.
        public static void Example05()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
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
