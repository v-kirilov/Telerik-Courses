using System;
using System.Collections.Generic;
using System.Linq;

namespace P01_Variations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            List<char> input = Console.ReadLine().Split(' ').Select(char.Parse).OrderBy(x => x).ToList();


            string write = string.Empty;
            Variations(input, num,write);
        }

        private static void Variations(List<char> input, int num,string write)
        {
            if (num==0)
            {
                Console.WriteLine(write);
                return;
            }
            for (int i = 0; i < input.Count; i++)
            {
                string newWrite = write + input[i];
                Variations(input, num - 1, newWrite);
            }

        }
    }
}
