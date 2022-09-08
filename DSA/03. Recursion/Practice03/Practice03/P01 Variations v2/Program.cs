using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P01_Variations_v2
{
    internal class Program
    {
        private static List<char> input;
        private static StringBuilder sb = new StringBuilder();
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            input = Console.ReadLine().Split(' ').Select(char.Parse).OrderBy(x => x).ToList();


            string write = string.Empty;
            Variations(input, num, write);
            Console.WriteLine(sb.ToString());
        }

        private static void Variations(List<char> input, int num, string write)
        {
            if (num == 0)
            {
                sb.AppendLine(write);
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
