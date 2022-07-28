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

            var lowNums = numbers.Where(num => num < 5);

            Console.WriteLine("Numbers < 5:");
            foreach (var x in lowNums)
            {
                Console.WriteLine(x);
            }
        }

        // This sample uses the where clause to find all products that are out of stock.
        public static void Example02()
        {
            var products = Database.Products;

            var soldOutProducts = products.Where(prod => prod.UnitsInStock == 0);

            Console.WriteLine("Sold out products:");
            foreach (var product in soldOutProducts)
            {
                Console.WriteLine("{0} is sold out!", product.ProductName);
            }
        }

        // This sample uses the where clause to find all products that are in stock and cost more than 3.00 per unit.
        public static void Example03()
        {
            var products = Database.Products;

            var expensiveInStockProducts = products.Where(prod => prod.UnitsInStock > 0 && prod.UnitPrice > 3.00M);

            Console.WriteLine("In-stock products that cost more than 3.00:");
            foreach (var product in expensiveInStockProducts)
            {
                Console.WriteLine("{0} is in stock and costs more than 3.00.", product.ProductName);
            }
        }

        // This sample uses the where clause to find all customers in Washington and then it uses a foreach loop to iterate over the orders collection that belongs to each customer.
        public static void Example04()
        {
            var customers = Database.Customers;

            var waCustomers = customers.Where(cust => cust.Region == "WA");

            Console.WriteLine("Customers from Washington and their orders:");
            foreach (var customer in waCustomers)
            {
                Console.WriteLine("Customer {0}: {1}", customer.CustomerID, customer.CompanyName);
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("  Order {0}: {1}", order.OrderID, order.OrderDate);
                }
            }
        }

        // This sample demonstrates an indexed where clause that returns digits whose name is shorter than their value.
        public static void Example05()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var shortDigits = digits.Where((digit, index) => digit.Length < index);

            Console.WriteLine("Short digits:");
            foreach (var d in shortDigits)
            {
                Console.WriteLine("The word {0} is shorter than its value.", d);
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
        }
    }
}
