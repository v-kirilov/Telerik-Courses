using System;

namespace P04_Zig_Zag
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');

            int n = int.Parse(input[0]);
            int m = int.Parse(input[1]);

            long[,] matrix = new long[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        matrix[i, j] = 1;
                    }
                    else if (j == 0)
                    {
                        matrix[i, j] = matrix[i - 1, j] + 3;
                    }
                    else
                    {
                        matrix[i, j] = matrix[i, j - 1] + 3;
                    }
                }
            }
            int currentRow = 0;
            int currentCol = 0;
            long sum = 1;

            int nextRow = currentRow;
            int nextCol = currentCol;

            for (int i = 0; i < matrix.GetLength(1) / 2; i++)
            {
                while (true)
                {
                    if (nextRow + 1 < matrix.GetLength(0) && nextCol + 1 < matrix.GetLength(1))
                    {
                        nextRow++;
                        nextCol++;
                        sum += matrix[nextRow, nextCol];
                    }
                    else
                    {
                        break;
                    }
                    if (nextCol + 1 < matrix.GetLength(1))
                    {
                        nextCol++;
                        nextRow--;
                        sum += matrix[nextRow, nextCol];
                    }
                    else
                    {
                        break;
                    }
                }
                while (true)
                {
                    if (nextRow + 1 < matrix.GetLength(0) && nextCol - 1 >= 0)
                    {
                        nextRow++;
                        nextCol--;
                        sum += matrix[nextRow, nextCol];
                    }
                    else
                    {
                        break;
                    }
                    if (nextCol - 1 >= 0)
                    {
                        nextRow--;
                        nextCol--;
                        sum += matrix[nextRow, nextCol];
                    }
                    else
                    {
                        break;
                    }
                }
                if (nextCol == 0 && nextRow == matrix.GetLength(0) - 1)
                {
                    break;
                }
            }
            Console.WriteLine(sum);
        }
    }
}
