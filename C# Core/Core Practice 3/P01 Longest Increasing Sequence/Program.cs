using System;

namespace P01_Longest_Increasing_Sequence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int pastNumber = 0;
            int counter = 1;
            int maxCounter = 0;
            for (int i = 0; i < n; i++)
            {
                int currentNum = int.Parse(Console.ReadLine());
                if (i == 0)
                {
                    pastNumber = currentNum;
                    continue;
                }
                if (currentNum > pastNumber)
                {
                    counter++;
                    if (counter > maxCounter)
                    {
                        maxCounter = counter;
                    }
                }
                else
                {
                    counter = 1;
                }
                pastNumber = currentNum;
            }
            

            Console.WriteLine(maxCounter);
        }
    }
}
