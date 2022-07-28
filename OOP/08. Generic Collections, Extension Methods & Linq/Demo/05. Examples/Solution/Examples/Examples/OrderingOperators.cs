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

            var sortedWords = words.OrderBy(word => word);

            Console.WriteLine("The sorted list of words:");
            foreach (var w in sortedWords)
            {
                Console.WriteLine(w);
            }
        }

        // This sample uses orderby to sort a list of words by length.
        public static void Example02()
        {
            string[] words = { "cherry", "apple", "blueberry" };

            var sortedWords = words.OrderBy(word => word.Length);

            Console.WriteLine("The sorted list of words (by length):");
            foreach (var w in sortedWords)
            {
                Console.WriteLine(w);
            }
        }

        // This sample uses orderby to sort a list of products by name. " +
        // Use the \"descending\" keyword at the end of the clause to perform a reverse ordering.
        public static void Example03()
        {
            var products = Database.Products;

            var sortedProducts = products.OrderBy(prod => prod.ProductName);

            foreach (var item in sortedProducts)
            {
                Console.WriteLine(item.ProductName);
            }
        }

        // This sample uses an OrderBy clause with a custom comparer to " +
        // do a case-insensitive sort of the words in an array.
        public static void Example04()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords = words.OrderBy(a => a, new CaseInsensitiveComparer());

            foreach (var item in sortedWords)
            {
                Console.WriteLine(item);
            }
        }
        // This sample uses orderby and descending to sort a list of " +
        // doubles from highest to lowest.

        public static void Example05()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            var sortedDoubles = doubles.OrderByDescending(d => d);

            Console.WriteLine("The doubles from highest to lowest:");
            foreach (var d in sortedDoubles)
            {
                Console.WriteLine(d);
            }
        }
        // This sample uses orderby to sort a list of products by units in stock " +
        // from highest to lowest.

        public static void Example06()
        {
            var products = Database.Products;
            var sortedProducts = products.OrderByDescending(prod => prod.UnitsInStock);

            foreach (var item in sortedProducts)
            {
                Console.WriteLine(item.ProductName);
            }
        }

        // This sample uses method syntax to call OrderByDescending because it " +
        // enables you to use a custom comparer.
        public static void Example07()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords = words.OrderByDescending(a => a, new CaseInsensitiveComparer());

            foreach (var item in sortedWords)
            {
                Console.WriteLine(item);
            }
        }

        // This sample uses a compound orderby to sort a list of digits, " +
        // first by length of their name, and then alphabetically by the name itself.
        public static void Example08()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var sortedDigits = digits.OrderBy(digit => digit.Length).ThenBy(digit => digit);

            Console.WriteLine("Sorted digits:");
            foreach (var d in sortedDigits)
            {
                Console.WriteLine(d);
            }
        }

        // The first query in this sample uses method syntax to call OrderBy and ThenBy with a custom comparer to " +
        // sort first by word length and then by a case-insensitive sort of the words in an array. " +
        // The second two queries show another way to perform the same task.
        public static void Example09()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords =
                words.OrderBy(a => a.Length)
                     .ThenBy(a => a, new CaseInsensitiveComparer());

            // Another way. 
            var sortedWords2 = words.OrderBy(word => word.Length);
            var sortedWords3 = sortedWords2.ThenBy(a => a, new CaseInsensitiveComparer());

            foreach (var d in sortedWords)
            {
                Console.WriteLine(d);
            }
        }

        // This sample uses a compound orderby to sort a list of products, " +
        // first by category, and then by unit price, from highest to lowest.
        public static void Example10()
        {
            var products = Database.Products;
            var sortedProducts = products.OrderBy(prod => prod.Category).ThenByDescending(prod => prod.UnitPrice);

            foreach (var d in sortedProducts)
            {
                Console.WriteLine(d.ProductName);
            }
        }

        // This sample uses an OrderBy and a ThenBy clause with a custom comparer to " +
        // sort first by word length and then by a case-insensitive descending sort " +
        // of the words in an array.
        public static void Example11()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedWords = words.OrderBy(a => a.Length).ThenByDescending(a => a, new CaseInsensitiveComparer());

            foreach (var d in sortedWords)
            {
                Console.WriteLine(d);
            }
        }
        // This sample uses Reverse to create a list of all digits in the array whose " +
        // second letter is 'i' that is reversed from the order in the original array.
        public static void Example12()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var reversedIDigits = (digits.Where(digit => digit[1] == 'i')).Reverse();

            Console.WriteLine("A backwards list of the digits with a second character of 'i':");
            foreach (var d in reversedIDigits)
            {
                Console.WriteLine(d);
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
