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
            int uniqueFactors = primeFactorsOf300.Distinct().Count();
            Console.WriteLine("There are {0} unique prime factors of 300.", uniqueFactors);
        }

        // This sample uses Count to get the number of odd ints in the array.
        public static void Example02()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int oddNumbers = numbers.Count(n => n % 2 == 1);
            Console.WriteLine("There are {0} odd numbers in the list.", oddNumbers);
        }

        // This sample uses Select and Count to return a list of customers and how many orders each has.
        public static void Example03()
        {
            var orderCounts = Database.Customers
                .Select(cust => new
                {
                    cust.CustomerID,
                    OrderCount = cust.Orders.Count()
                });

            foreach (var item in orderCounts)
            {
                Console.WriteLine(item);
            }
        }

        // This sample uses Sum to add all the numbers in an array.
        public static void Example04()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            double numSum = numbers.Sum();
            Console.WriteLine("The sum of the numbers is {0}.", numSum);
        }

        // This sample uses Sum to get the total number of characters of all words in the array.
        public static void Example05()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            double totalChars = words.Sum(w => w.Length);
            Console.WriteLine("There are a total of {0} characters in these words.", totalChars);
        }

        // This sample uses Sum to get the total units in stock for each product category.
        public static void Example06()
        {
            var categories = Database.Products.GroupBy(prod => prod.Category).
                                Select(prodGroup => new
                                {
                                    Category = prodGroup.Key,
                                    TotalUnitsInStock = prodGroup.Sum(p => p.UnitsInStock)
                                });

            foreach (var item in categories)
            {
                Console.WriteLine(item);
            }
        }

        // This sample uses Min to get the lowest number in an array.
        public static void Example07()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int minNum = numbers.Min();

            Console.WriteLine("The minimum number is {0}.", minNum);
        }

        // This sample uses Min to get the length of the shortest word in an array.
        public static void Example08()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            int shortestWord = words.Min(w => w.Length);

            Console.WriteLine("The shortest word is {0} characters long.", shortestWord);
        }

        // This sample uses Min to get the cheapest price among each category's DbContext.Products.
        public static void Example09()
        {
            var categories = Database.Products.GroupBy(prod => prod.Category)
                .Select(prodGroup => new
                {
                    Category = prodGroup.Key,
                    CheapestPrice = prodGroup.Min(p => p.UnitPrice)
                });

            foreach (var item in categories)
            {
                Console.WriteLine(item);
            }
        }

        // This sample uses Max to get the highest number in an array. Note that the method returns a single value.
        public static void Example10()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int maxNum = numbers.Max();

            Console.WriteLine("The maximum number is {0}.", maxNum);
        }

        // This sample uses Max to get the length of the longest word in an array.
        public static void Example11()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            int longestLength = words.Max(w => w.Length);

            Console.WriteLine("The longest word is {0} characters long.", longestLength);
        }

        // This sample uses Max to get the most expensive price among each category's DbContext.Products.
        public static void Example12()
        {
            var categories = Database.Products.GroupBy(prod => prod.Category)
                .Select(prodGroup => new
                {
                    Category = prodGroup.Key,
                    MostExpensivePrice = prodGroup.Max(p => p.UnitPrice)
                });

            foreach (var item in categories)
            {
                Console.WriteLine(item);
            }
        }

        // This sample uses Average to get the average of all numbers in an array.
        public static void Example13()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            double averageNum = numbers.Average();

            Console.WriteLine("The average number is {0}.", averageNum);
        }
        // This sample uses Average to get the average length of the words in the array.
        public static void Example14()
        {
            string[] words = { "cherry", "apple", "blueberry" };
            double averageLength = words.Average(w => w.Length);

            Console.WriteLine("The average word length is {0} characters.", averageLength);
        }

        public static void Example15()
        {
            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };
            double product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);

            Console.WriteLine("Total product of all numbers: {0}", product);
        }
        // This sample uses Aggregate to create a running account balance that subtracts each withdrawal from the initial balance of 100, as long as the balance never drops below 0.
        public static void Example16()
        {
            double startBalance = 100.0;
            int[] attemptedWithdrawals = { 20, 10, 40, 50, 10, 70, 30 };
            double endBalance =
                attemptedWithdrawals.Aggregate(startBalance,
                    (balance, nextWithdrawal) =>
                    ((nextWithdrawal <= balance) ? (balance - nextWithdrawal) : balance));

            Console.WriteLine("Ending balance: {0}", endBalance);
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
