using System;
using System.Linq;

namespace P05_Three_Groups
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i]%3==0)
                {
                    Console.Write($"{arr[i]} ");
                }
            }
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 3 == 1)
                {
                    Console.Write($"{arr[i]} ");
                }
            }
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] % 3 == 2)
                {
                    Console.Write($"{arr[i]} ");
                }
            }
        }
    }
}
