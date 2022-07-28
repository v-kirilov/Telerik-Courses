using System.Linq;
using System;
using Linq.Examples.Data;
using System.Collections.Generic;

namespace Linq.Examples
{
    public static class MiscellaneousOperators
    {
        // This sample uses Concat to create one sequence that contains each array's values, one after the other.")]
        public static void Example01()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var allNumbers = numbersA.Concat(numbersB);

            Console.WriteLine("All numbers from both arrays:");
            foreach (var n in allNumbers)
            {
                Console.WriteLine(n);
            }
        }
        // This sample uses Concat to create one sequence that contains the names of all customers and products, including any duplicates.")]
        public static void Example02()
        {
            var  customers =Database.Customers;
            var products = Database.Products;

            var customerNames = customers.Select(cust => cust.CompanyName);
            var productNames = products.Select(prod => prod.ProductName);

            var allNames = customerNames.Concat(productNames);

            Console.WriteLine("Customer and product names:");
            foreach (var n in allNames)
            {
                Console.WriteLine(n);
            }
        }
        // This sample uses SequenceEquals to see if two sequences match on all elements in the same order.")]
        public static void Example03()
        {
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "cherry", "apple", "blueberry" };

            bool match = wordsA.SequenceEqual(wordsB);

            Console.WriteLine("The sequences match: {0}", match);
        }
        // This sample uses SequenceEqual to see if two sequences match on all elements in the same order.")]
        public static void Example04()
        {
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "apple", "blueberry", "cherry" };

            bool match = wordsA.SequenceEqual(wordsB);

            Console.WriteLine("The sequences match: {0}", match);
        }
        // Run all examples
        public static void RunAllExamples()
        {
            Example01();
            Example02();
            Example03();
            Example04();
        }
    }
}
