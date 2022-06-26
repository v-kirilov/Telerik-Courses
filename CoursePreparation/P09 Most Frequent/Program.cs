using System;
using System.Collections.Generic;

namespace P09_Most_Frequent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int topNumber = 0;
            int maxCount=0;
            for (int i = 0; i < count; i++)
            {
                int number = int.Parse(Console.ReadLine());
                if (dict.ContainsKey(number))
                {
                    dict[number]++;
                }
                else
                {
                dict[number] = 1;

                }
            }
            foreach (var num in dict)
            {
                if (num.Value>maxCount)
                {
                    topNumber = num.Key;
                    maxCount = num.Value;
                }
            }

            Console.WriteLine($"{topNumber} ({maxCount} times)");
        }
    }
}
