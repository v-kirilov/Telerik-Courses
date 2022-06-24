using System;
using System.Collections.Generic;
using System.Linq;

namespace P06_Array_Search
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] inputArr = Console.ReadLine()
                .Split(',')
                .Select(int.Parse)
                .ToArray();
            List<int> output = new List<int>();

            for (int i = 1; i <= inputArr.Length; i++)
            {
                if (!inputArr.Contains(i))
                {
                    output.Add(i);
                }
            }
            output.Sort();
            Console.WriteLine(String.Join(",",output));
        }

     
    }
}
