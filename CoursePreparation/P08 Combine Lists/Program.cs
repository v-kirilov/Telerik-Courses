using System;
using System.Collections.Generic;
using System.Linq;

namespace P08_Combine_Lists
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> listOne = Console.ReadLine().Split(',').Select(int.Parse).ToList();
            List<int> listTwo = Console.ReadLine().Split(',').Select(int.Parse).ToList();
            List<int> merged = new List<int>();
            for(int i = 0; i < listOne.Count; i++)
            {
                merged.Add(listOne[i]);
                merged.Add(listTwo[i]);
            }
            Console.WriteLine(String.Join(",",merged));
        }
    }
}
