using System;
using System.Linq;

namespace P02_Symmetric_Arrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            for (int j = 0; j < num; j++)
            {
                int[] arr = Console.ReadLine()
               .Split(' ')
               .Select(int.Parse)
               .ToArray();
                bool isMirrored = true;
                for (int i = 0; i < arr.Length / 2; i++)
                {
                   
                    if (arr[i] != arr[arr.Length - i - 1])
                    {
                        isMirrored = false;
                        break;
                    }
                }
                if (isMirrored)
                {
                    Console.WriteLine("Yes");
                }
                else
                {
                    Console.WriteLine("No");
                }
            }
           
        }
    }
}
