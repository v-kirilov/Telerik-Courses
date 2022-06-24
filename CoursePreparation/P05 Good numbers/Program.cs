using System;
using System.Linq;

namespace P05_Good_numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            int[] input = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            int start = input[0];
            int end = input[1];
            for (int i = start; i <= end; i++)
            {
                if (i <= 10)
                {
                    counter++;
                    continue;
                }
                else
                {
                    bool isGood = true;
                    int number = i;
                    while (number != 0)
                    {
                        int divider = number % 10;
                        if (divider == 0)
                        {
                            number = number / 10;
                            continue;
                        }
                        if (i % divider != 0)
                        {
                            isGood = false;
                            break;
                        }
                        number = number / 10;
                    }
                    if (isGood)
                    {
                        counter++;
                    }
                }
            }
            Console.WriteLine(counter);
        }
    }
}
