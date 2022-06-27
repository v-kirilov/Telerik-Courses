using System;
using System.Numerics;

namespace P07_Above_the_Main_Diagonal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int dim = int.Parse(Console.ReadLine());
            long[,] matrix = new long[dim, dim];
            BigInteger sum = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == j && i == 0)
                    {
                        matrix[i, j] = 1;
                    }
                    else if (i == 0 && j == 1)
                    {
                        matrix[i, j] = 2;
                    }
                    else if (i == 0)
                    {
                        matrix[i, j] = matrix[i, j - 1] * 2;
                    }
                    else if (i != 0 && j == 0)
                    {
                        matrix[i, j] = matrix[i - 1, j] * 2;
                    }
                    else
                    {
                        matrix[i, j] = matrix[i, j - 1] * 2;
                    }

                    if (j > i)
                    {
                        sum += matrix[i, j];
                    }
                }
            }
            Console.WriteLine(sum);
        }
    }
}