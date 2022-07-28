using Linq.Examples.Data;
using System;
using System.Linq;

namespace Linq.Examples
{
    public static class AggregationOperators
    {
        // This sample uses Count to get the number of unique prime factors of 300.
        public static void Example01()
        {
            int[] primeFactorsOf300 = { 2, 2, 3, 5, 5 };
            //Your implementation goes here
        }

        // This sample uses Count to get the number of odd ints in the array.
        public static void Example02()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }

        // This sample uses Select and Count to return a list of customers and how many orders each has.
        public static void Example03()
        {
            var customers = Database.Customers;
            //Your implementation goes here
        }

        // This sample uses Sum to add all the numbers in an array.
        public static void Example04()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }

        // This sample uses Sum to get the total number of characters of all words in the array.
        public static void Example05()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            //Your implementation goes here
        }

        // This sample uses Sum to get the total units in stock for each product category.
        public static void Example06()
        {
            var products = Database.Products;
            //Your implementation goes here
        }

        // This sample uses Min to get the lowest number in an array.
        public static void Example07()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }

        // This sample uses Min to get the length of the shortest word in an array.
        public static void Example08()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            //Your implementation goes here
        }

        // This sample uses Min to get the cheapest price among each category's DbContext.Products.
        public static void Example09()
        {
            var products = Database.Products;
            //Your implementation goes here
        }

        // This sample uses Max to get the highest number in an array. Note that the method returns a single value.
        public static void Example10()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }

        // This sample uses Max to get the length of the longest word in an array.
        public static void Example11()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            //Your implementation goes here
        }

        // This sample uses Max to get the most expensive price among each category's DbContext.Products.
        public static void Example12()
        {
            var products = Database.Products;
            //Your implementation goes here
        }

        // This sample uses Average to get the average of all numbers in an array.
        public static void Example13()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //Your implementation goes here
        }
        // This sample uses Average to get the average length of the words in the array.
        public static void Example14()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            //Your implementation goes here
        }

        public static void Example15()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            //Your implementation goes here
        }
        // This sample uses Aggregate to create a running account balance that subtracts each withdrawal from the initial balance of 100, as long as the balance never drops below 0.
        public static void Example16()
        {
            double startBalance = 100.0;
            int[] attemptedWithdrawals = { 20, 10, 40, 50, 10, 70, 30 };
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
            Example06();
            Example07();
            Example08();
            Example09();
            Example11();
            Example10();
            Example11();
            Example12();
            Example13();
            Example14();
            Example15();
            Example16();
        }
    }
}
