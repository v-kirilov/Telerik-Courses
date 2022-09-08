using System;
using System.Linq;

namespace P01_ProblemSolving
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            int rows = arr[0];
            int cols = arr[1];
            int[,] field = new int[rows, cols];
            int startCol = 0;
            int startRow = 0;

            for (int i = 0; i < rows; i++)
            {
                int[] coinsArr = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
                for (int j = 0; j < cols; j++)
                {
                    if (coinsArr[j] == 0)
                    {
                        startRow = i;
                        startCol = j;
                    }
                    field[i, j] = coinsArr[j];
                }
            }

            Console.WriteLine(CollectCoins(field,startRow,startCol));

        }

        static int CollectCoins(int[,] field, int row, int col)
        {
            int coldDirection = 0;
            int rowDirection = 0;
            int maxElement = 0;

            //try left, right,up , down
            //try left
            //First check if we are in bounds of the matrix , and than if the element is bigger, that way it wont throw exception!!!! If the first check returns false it will stop searching!!!
            if (col - 1 >= 0 && field[row, col - 1] > maxElement)
            {
                coldDirection = -1;
                maxElement = field[row, col - 1];

            }
            //try right
            if (col + 1 < field.GetLength(1) && field[row, col + 1] > maxElement)
            {
                coldDirection = 1;
                maxElement = field[row, col + 1];

            }
            //try up
            if (row - 1 >= 0 && field[row - 1, col] > maxElement)
            {
                rowDirection = -1;
                coldDirection = 0; //So that we wont go diagonal we have to return it to 0
                maxElement = field[row - 1, col];

            }

            //try down
            if (row + 1 < field.GetLength(0) && field[row + 1, col] > maxElement)
            {
                rowDirection = 1;
                coldDirection = 0; //So that we wont go diagonal we have to return it to 0
                maxElement = field[row + 1, col];

            }

            //if maxElement =0, return 0
            if (maxElement == 0)
            {
                return 0;
            }

            //adjust next row and col 
            int nextRow = row + rowDirection;
            int nextCol = col + coldDirection;

            field[nextRow, nextCol]--;


            return 1 + CollectCoins(field, nextRow, nextCol);
        }
    }
}
