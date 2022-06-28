using System;

namespace P02_Longest_Sequence_of_Equal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            int maxCount = 1;
            int count = 1;
            int pastNumber = 0;
            for (int i = 0; i < num; i++)
            {
                int newNumber = int.Parse(Console.ReadLine());
                if (i == 0)
                {
                    pastNumber = newNumber;
                    continue;

                }
                if (pastNumber == newNumber)
                {
                    count++;
                    if (count > maxCount)
                    {
                        maxCount = count;
                    }
                }
                else
                {
                    count = 1;
                }
                pastNumber = newNumber;
            }

            Console.WriteLine(maxCount);
        }
    }
}
