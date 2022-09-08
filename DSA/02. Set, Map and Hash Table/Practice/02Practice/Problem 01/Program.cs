using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            SortedDictionary<string, int> dict = new SortedDictionary<string, int>();
            for (int i = 0; i < num; i++)
            {
                string animal = Console.ReadLine();
                if (dict.ContainsKey(animal))
                {
                    dict[animal]++;
                    continue;
                }
                dict[animal] = 1;
            }
            foreach (var animal in dict)
            {
                
                string isEven = dict[animal.Key] % 2 == 0 ? "Yes" : "No";

                Console.WriteLine($"{animal.Key} {animal.Value} {isEven}");
            }
        }
    }
}

