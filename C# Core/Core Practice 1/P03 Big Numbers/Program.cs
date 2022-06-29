using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace P03_Big_Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] inputArr = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            int[] firstArr = new int[inputArr[0]];
            int[] secondArr = new int[inputArr[1]];

            firstArr = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            secondArr = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int maxLength = 0;
            if (firstArr.Length >= secondArr.Length)
            {
                maxLength = firstArr.Length;
            }
            else
            {
                maxLength = secondArr.Length;
            }

            int counter = 0;
            List<int> result = new List<int>();

            for (int i = 0; i < maxLength; i++)
            {
                int number = 0;
                if (i < firstArr.Length && i < secondArr.Length)
                {
                    number = firstArr[i] + secondArr[i];
                    if (counter != 0)
                    {
                        number++;
                        counter = 0;
                    }
                    if (number >= 10)
                    {
                        result.Add(number - 10);
                        counter = 1;
                    }
                    else
                    {
                        result.Add(number);
                    }
                }
                else if (i >= firstArr.Length)
                {
                    number = secondArr[i];
                    if (counter != 0)
                    {
                        number++;
                        counter = 0;
                    }
                    if (number >= 10)
                    {
                        result.Add(number - 10);
                        counter = 1;
                    }
                    else
                    {
                        result.Add(number);
                    }
                }
                else if (i >= secondArr.Length)
                {
                    number = firstArr[i];
                    if (counter != 0)
                    {
                        number++;
                        counter = 0;
                    }
                    if (number >= 10)
                    {
                        result.Add(number - 10);
                        counter = 1;
                    }
                    else
                    {
                        result.Add(number);
                    }
                }
            }

            if (counter==1)
            {
                result.Add(1);
            }
            
            Console.WriteLine(String.Join(" ",result));
        }
    }
}
