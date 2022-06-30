using System;
using System.Linq;

namespace P04_Bounce
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            int rows = input[0];
            int cols = input[1];

            long[,] matrix = new long[rows, cols];


            //Fill matrix
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {

                    matrix[row, col] = (long)Math.Pow(2, row + col);

                }
            }

            int currentRow = 0;
            int currentCol = 0;

            int rowDirection = 1;  //Can be 1 or -1
            int colDirection = 1;  //Can be 1 or -1

            long result = matrix[currentRow, currentCol];

            if (rows == 1 || cols == 1)
            {
                Console.WriteLine(result);
                Environment.Exit(0);
            }

            while (true)
            {
                int potentialRow = currentRow + rowDirection;
                int potentialCol = currentCol + colDirection;

                if (potentialRow < 0)
                {
                    rowDirection = 1;
                }
                if (potentialRow >= matrix.GetLength(0))
                {
                    rowDirection = -1;
                }
                if (potentialCol < 0)
                {
                    colDirection = 1;
                }
                if (potentialCol >= matrix.GetLength(1))
                {
                    colDirection = -1;
                }
                currentRow += rowDirection;
                currentCol += colDirection;
                result += matrix[currentRow, currentCol];

                if (IsCorner(matrix, currentRow, currentCol))
                {
                    Console.WriteLine(result);
                    Environment.Exit(0);
                }

            }

            Console.WriteLine();
        }

        static bool IsCorner(long[,] matrix, int currentRow, int CurrentCol)
        {
            if (currentRow == 0 && CurrentCol == 0)
            {
                return true;
            }
            else if (currentRow == 0 && CurrentCol == matrix.GetLength(1) - 1)
            {
                return true;
            }
            else if (currentRow == matrix.GetLength(0) - 1 && CurrentCol == 0)
            {
                return true;
            }
            else if (currentRow == matrix.GetLength(0) - 1 && CurrentCol == matrix.GetLength(1) - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
