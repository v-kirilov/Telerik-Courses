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
            //Your implementation goes here
        }
        // This sample uses Concat to create one sequence that contains the names of all customers and products, including any duplicates.")]
        public static void Example02()
        {
            var  customers =Database.Customers;
            var products = Database.Products;
            //Your implementation goes here
        }
        // This sample uses SequenceEquals to see if two sequences match on all elements in the same order.")]
        public static void Example03()
        {
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "cherry", "apple", "blueberry" };
            //Your implementation goes here
        }
        // This sample uses SequenceEqual to see if two sequences match on all elements in the same order.")]
        public static void Example04()
        {
            var wordsA = new string[] { "cherry", "apple", "blueberry" };
            var wordsB = new string[] { "apple", "blueberry", "cherry" };
            //Your implementation goes here
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
