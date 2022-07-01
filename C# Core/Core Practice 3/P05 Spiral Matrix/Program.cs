using System;

namespace P05_Spiral_Matrix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            long[,] matrix = new long[n, n];

            int currentRow = 0;
            int currentCol = 0;
            matrix[currentRow, currentCol] = 1;
            if (n==1)
            {
                PrintMatrix(matrix);
                return;
            }
            long counter = 2;
            while (true)
            {
                while (currentCol < matrix.GetLength(1))
                {
                    currentCol++;
                    if (currentCol==matrix.GetLength(1) || matrix[currentRow,currentCol]!=0)
                    {
                        currentCol--;
                        break;
                    }
                    matrix[currentRow, currentCol] += counter;
                    counter++;
                }
                while (currentRow < matrix.GetLength(0))
                {
                    currentRow++;
                    if (matrix[currentRow, currentCol] != 0)
                    {
                        currentRow--;
                        break;
                    }
                    matrix[currentRow, currentCol] += counter;
                    counter++;
                    if (currentRow == matrix.GetLength(0) - 1)
                    {
                        break;
                    }
                }
                while (currentCol >= 0)
                {
                    currentCol--;
                    if (currentCol < 0 || matrix[currentRow, currentCol] != 0)
                    {
                        currentCol++;
                        break;
                    }
                    matrix[currentRow, currentCol] += counter;
                    counter++;

                }
                while (currentRow >= 0)
                {
                    currentRow--;
                    if (currentRow < 0 || matrix[currentRow, currentCol] != 0)
                    {
                        currentRow++;
                        break;
                    }
                    matrix[currentRow, currentCol] += counter;
                    counter++;
                }


                if (IsMatrixFull(matrix))
                {
                    break;
                }
            }

            PrintMatrix(matrix);
        }

        static bool IsMatrixFull(long[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static void PrintMatrix(long[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
