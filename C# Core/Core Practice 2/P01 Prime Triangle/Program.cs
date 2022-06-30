using System;

namespace Telerik___Mock_Exam_Problem_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int howManyPrimeNumbers = 0;

            for (int i = 1; i <= n; i++)
            {
                bool isPrime = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    howManyPrimeNumbers++;
                }
            }

            for (int i = 1; i <= howManyPrimeNumbers; i++)
            {
                int row = i;
                for (int j = 1; j <= row; j++)
                {
                    if (!IsPrime(j))
                    {
                        row++;
                        Console.Write($"{0}");
                        continue;
                    }
                    Console.Write($"{1}");

                }

                Console.WriteLine();
            }

        }

        static bool IsPrime(int num)
        {
            for (int i = 2; i < num; i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}