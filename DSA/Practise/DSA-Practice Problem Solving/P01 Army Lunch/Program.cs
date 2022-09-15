using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P01_Army_Lunch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<string> list = new List<string>(n);

            list = Console.ReadLine().Split(' ').ToList();
            
            List<string> first = new List<string>(n);

            for (int i = 0; i < n; i++)
            {
                if (list[i][0] == 'S')
                {
                    first.Add(list[i]);
                }
            }

            List<string> second = new List<string>(n);
            for (int i = 0; i < n; i++)
            {
                if (list[i][0] == 'C')
                {
                    second.Add(list[i]);
                }
            }

            List<string> third = new List<string>(n);
            for (int i = 0; i < n; i++)
            {
                if (list[i][0] == 'P')
                {
                    third.Add(list[i]);
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var word in first)
            {
                sb.Append(word + " ");
            }
            foreach (var word in second)
            {
                sb.Append(word + " ");
            }
            foreach (var word in third)
            {
                sb.Append(word + " ");
            }
            sb.Remove(sb.Length-1,1);
            Console.WriteLine(sb.ToString());
        }
    }
}
