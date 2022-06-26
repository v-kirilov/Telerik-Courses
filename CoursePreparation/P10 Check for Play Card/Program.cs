using System;
using System.Linq;

namespace P10_Check_for_Play_Card
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] arr = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string input = Console.ReadLine();
            if (arr.Contains(input))
            {
                Console.WriteLine($"yes {input}");
            }else
            {
                Console.WriteLine($"no {input}");

            }

        }
    }
}
