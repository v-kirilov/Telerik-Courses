using System;
using System.Collections.Generic;
using System.Linq;

namespace P03_Least_Majority_Multiple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(int.Parse(Console.ReadLine()));
            }
            int maxNumber = 0;
            int maxCounter = 0;
            //Console.WriteLine(String.Join(" ",list));
            for (int i = 0; i < list.Count; i++)
            {
                int number = list[i];
                if (number ==1 )
                {
                    continue;
                }
                int counter = 0;
                for (int j = 0; j < list.Count; j++)
                {
                    int currentNum = list[j];
                    if (number%currentNum==0)
                    {
                        counter++;
                    }
                    if (counter > maxCounter)
                    {
                        maxCounter = counter;
                        maxNumber= number;
                    }
                }
            }
            if (maxCounter>=3)
            {
                Console.WriteLine(maxNumber);
                Environment.Exit(0);
            }else
            {
                int multiplier = 2;
                int minNumber = list.Min();
                int counter = 0;

                while (true)
                {
                    int candidate = multiplier * minNumber;
                    counter = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        int currentNum = list[i];
                        if (candidate % currentNum == 0)
                        {
                            counter++;
                        }
                        if (counter>maxCounter)
                        {
                            maxCounter = counter;
                            maxNumber = candidate;
                        }
                    }
                    if (counter<3)
                    {
                        multiplier++;
                    }
                    if (counter>=3)
                    {
                        Console.WriteLine(maxNumber);
                        break;
                    }
                }
            }


        }
    }
}
