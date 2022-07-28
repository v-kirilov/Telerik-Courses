using Linq.Examples.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq.Examples
{
    public class OrderingOperators
    {
        // This sample uses orderby to sort a list of words alphabetically.
        public static void Example01()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            //Your implementation goes here
        }

        // This sample uses orderby to sort a list of words by length.
        public static void Example02()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            //Your implementation goes here
        }

        // This sample uses orderby to sort a list of products by name. " +
        // Use the \"descending\" keyword at the end of the clause to perform a reverse ordering.
        public static void Example03()
        {
            var products = Database.Products;
            //Your implementation goes here
        }

        // This sample uses an OrderBy clause with a custom comparer to " +
        // do a case-insensitive sort of the words in an array.
        public static void Example04()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            //Your implementation goes here
        }

        // This sample uses orderby and descending to sort a list of " +
        // doubles from highest to lowest.
        public static void Example05()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            //Your implementation goes here
        }
        // This sample uses orderby to sort a list of products by units in stock " +
        // from highest to lowest.

        public static void Example06()
        {
            var products = Database.Products;
            //Your implementation goes here
        }

        // This sample uses method syntax to call OrderByDescending because it " +
        // enables you to use a custom comparer.
        public static void Example07()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            //Your implementation goes here
        }

        // This sample uses a compound orderby to sort a list of digits, " +
        // first by length of their name, and then alphabetically by the name itself.
        public static void Example08()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            //Your implementation goes here
        }

        // The first query in this sample uses method syntax to call OrderBy and ThenBy with a custom comparer to " +
        // sort first by word length and then by a case-insensitive sort of the words in an array. " +
        // The second two queries show another way to perform the same task.
        public static void Example09()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            //Your implementation goes here
        }

        // This sample uses a compound orderby to sort a list of products, " +
        // first by category, and then by unit price, from highest to lowest.
        public static void Example10()
        {
            var products = Database.Products;
            //Your implementation goes here
        }

        // This sample uses an OrderBy and a ThenBy clause with a custom comparer to " +
        // sort first by word length and then by a case-insensitive descending sort " +
        // of the words in an array.
        public static void Example11()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            //Your implementation goes here
        }
        // This sample uses Reverse to create a list of all digits in the array whose " +
        // second letter is 'i' that is reversed from the order in the original array.
        public static void Example12()
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
            Example06();
            Example07();
            Example08();
            Example09();
            Example10();
            Example11();
            Example12();
        }
    }

    // Custom comparer for use with ordering operators
    public class CaseInsensitiveComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
        }
    }
}
