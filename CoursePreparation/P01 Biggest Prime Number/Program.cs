using System;
using System.Collections.Generic;
using System.Linq;

namespace P01_Biggest_Prime_Number
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            int[] numbers =new int[number];
            for (int i = 0; i < number; i++)
            {
                numbers[i] = i+1;
            }
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i]==0)
                {
                    continue;
                }
                for (int j = i+1; j < numbers.Length; j+=i+1)
                {
                    if (j+i>numbers.Length-1)
                    {
                        break;
                    }
                    if (numbers[j+i]==0)
                    {
                        continue;
                    }
                    numbers[j+i] = 0;
                }

            }
            
            Console.WriteLine(numbers.Max());
        }
    }
}
