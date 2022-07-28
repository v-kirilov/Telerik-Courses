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
            //Your implementation goes here
        }
        // This sample uses Distinct to find the unique Category names.
        public static void Example02()
        {
            var products = Database.Products;
            //Your implementation goes here
        }
        // This sample uses Union to create one sequence that contains the unique values from both arrays.
        public static void Example03()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            //Your implementation goes here
        }
        // This sample uses the Union method to create one sequence that contains the unique first letter from both product and customer names. Union is only available through method syntax.
        public static void Example04()
        {
            var products = Database.Products;
            var customers = Database.Customers;
            //Your implementation goes here
        }
        // This sample uses Intersect to create one sequence that contains the common values shared by both arrays.
        public static void Example05()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            //Your implementation goes here
        }
        // This sample uses Intersect to create one sequence that contains the common first letter from both product and customer names.
        public static void Example06()
        {
            var products = Database.Products;
            var customers = Database.Customers;
            //Your implementation goes here
        }

        // This sample uses Except to create a sequence that contains the values from numbersA that are not also in numbersB.
        public static void Example07()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            //Your implementation goes here
        }

        // This sample uses Except to create one sequence that contains the first letters of product names that are not also first letters of customer names.
        public static void Example08()
        {
            var products = Database.Products;
            var customers = Database.Customers;
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
        }
    }
}
