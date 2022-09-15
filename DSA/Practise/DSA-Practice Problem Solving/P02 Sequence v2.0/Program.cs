using System;
using System.Collections.Generic;
using System.Linq;

namespace P02_Sequence_v2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            long k = input[0];
            long n = input[1];
            long mainNumber = k;
            List<long> list = new List<long>();
            list.Add(mainNumber);
            long counter = 0;
            int mainNumberCounter = 0;
            long currentNum = 0;

            for (int i = 1; i < n; i++)
            {

                if (i % 3 == 1)
                {
                    currentNum = mainNumber + 1;
                    list.Add(currentNum);
                }
                else if (i % 3 == 2)
                {
                    currentNum = 2 * mainNumber + 1;
                    list.Add(currentNum);

                }
                else if (i % 3 == 0)
                {
                    currentNum = mainNumber + 2;
                    list.Add(currentNum);

                }


                if (counter == 3)
                {
                    mainNumberCounter++;
                    mainNumber = list[mainNumberCounter];
                    counter = 0;
                }

                counter++;
            }

            Console.WriteLine(list[list.Count-1]);
        }
    }
}
