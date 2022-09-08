using System;
using System.Collections.Generic;
using System.Linq;

namespace P01_Cafe_Order_Checker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] takeOutOrders = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            int[] dineInOrders = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            int[] servedOrders = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            bool isTakeOutInOrder = CheckOrders(takeOutOrders, servedOrders);
            if (!isTakeOutInOrder)
            {
                Console.WriteLine("Not in order!");
                return;
            }

            bool isdineInOrders = CheckOrders(dineInOrders, servedOrders);
            if (!isTakeOutInOrder)
            {
                Console.WriteLine("Not in order!");
                return;
            }
            Console.WriteLine("In order!");
        }

        private static bool CheckOrders(int[] takeOutOrders, int[] servedOrders)
        {
            List<int> check = new List<int>();
            for (int i = 0; i < takeOutOrders.Length; i++)
            {
                for (int j = 0; j < servedOrders.Length; j++)
                {
                    if (takeOutOrders[i] == servedOrders[j])
                    {
                        check.Add(j);
                    }
                }
            }

            for (int i = 0; i < check.Count-1; i++)
            {
                if (check[i] > check[i+1])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
