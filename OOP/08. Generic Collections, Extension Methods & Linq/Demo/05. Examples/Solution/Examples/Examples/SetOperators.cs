using System.Linq;
using System;
using Linq.Examples.Data;
using System.Collections.Generic;

namespace Linq.Examples
{
    public static class SetOperators
    {
        // This sample uses Distinct to remove duplicate elements in a sequence of factors of 300.
        public static void Example01()
        {
            int[] factorsOf300 = { 2, 2, 3, 5, 5 };

            var uniqueFactors = factorsOf300.Distinct();

            Console.WriteLine("Prime factors of 300:");
            foreach (var f in uniqueFactors)
            {
                Console.WriteLine(f);
            }
        }
        // This sample uses Distinct to find the unique Category names.
        public static void Example02()
        {
            var products = Database.Products;

            var categoryNames = (products.Select(prod => prod.Category)).Distinct();

            Console.WriteLine("Category names:");
            foreach (var n in categoryNames)
            {
                Console.WriteLine(n);
            }
        }
        // This sample uses Union to create one sequence that contains the unique values from both arrays.
        public static void Example03()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var uniqueNumbers = numbersA.Union(numbersB);

            Console.WriteLine("Unique numbers from both arrays:");
            foreach (var n in uniqueNumbers)
            {
                Console.WriteLine(n);
            }
        }
        // This sample uses the Union method to create one sequence that contains the unique first letter from both product and customer names. Union is only available through method syntax.
        public static void Example04()
        {
            var products = Database.Products;
            var customers = Database.Customers;

            var productFirstChars = products.Select(prod => prod.ProductName[0]);
            var customerFirstChars = customers.Select(cust => cust.CompanyName[0]);

            var uniqueFirstChars = productFirstChars.Union(customerFirstChars);

            Console.WriteLine("Unique first letters from Product names and Customer names:");
            foreach (var ch in uniqueFirstChars)
            {
                Console.WriteLine(ch);
            }
        }
        // This sample uses Intersect to create one sequence that contains the common values shared by both arrays.
        public static void Example05()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var commonNumbers = numbersA.Intersect(numbersB);

            Console.WriteLine("Common numbers shared by both arrays:");
            foreach (var n in commonNumbers)
            {
                Console.WriteLine(n);
            }
        }
        // This sample uses Intersect to create one sequence that contains the common first letter from both product and customer names.
        public static void Example06()
        {
            var products = Database.Products;
            var customers = Database.Customers;

            var productFirstChars = products.Select(prod => prod.ProductName[0]);
            var customerFirstChars = customers.Select(cust => cust.CompanyName[0]);

            var commonFirstChars = productFirstChars.Intersect(customerFirstChars);

            Console.WriteLine("Common first letters from Product names and Customer names:");
            foreach (var ch in commonFirstChars)
            {
                Console.WriteLine(ch);
            }
        }

        // This sample uses Except to create a sequence that contains the values from numbersA that are not also in numbersB.
        public static void Example07()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            IEnumerable<int> aOnlyNumbers = numbersA.Except(numbersB);

            Console.WriteLine("Numbers in first array but not second array:");
            foreach (var n in aOnlyNumbers)
            {
                Console.WriteLine(n);
            }
        }

        // This sample uses Except to create one sequence that contains the first letters of product names that are not also first letters of customer names.
        public static void Example08()
        {
            var products = Database.Products;
            var customers = Database.Customers;

            var productFirstChars = products.Select(prod => prod.ProductName[0]);
            var customerFirstChars = customers.Select(cust => cust.CompanyName[0]);

            var productOnlyFirstChars = productFirstChars.Except(customerFirstChars);

            Console.WriteLine("First letters from Product names, but not from Customer names:");
            foreach (var ch in productOnlyFirstChars)
            {
                Console.WriteLine(ch);
            }
        }

        // Run all examples
        public static void RunAllExamples()
        {
            Example01();
            Example02();
            Example03();
            Example04();
            Example05();
            Example06();
            Example07();
            Example08();
        }
    }
}
