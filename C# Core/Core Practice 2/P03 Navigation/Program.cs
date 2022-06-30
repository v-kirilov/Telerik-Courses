using System;
using System.Linq;
using System.Numerics;

namespace P03_Navigation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int row = int.Parse(Console.ReadLine());
            int col = int.Parse(Console.ReadLine());
            int moveCount = int.Parse(Console.ReadLine());
            decimal[] movesArr = Console.ReadLine()
                .Split(' ')
                .Select(decimal.Parse)
                .ToArray();

            int coeff = Math.Max(row, col);

            BigInteger[,] matrix = new BigInteger[row, col];
            matrix[row - 1, 0] = 1;
            matrix[row - 1, 1] = 2;
            matrix[row - 2, 0] = 2;

            //Fill matrix backwords!
            for (int i = row - 1; i >= 0; i--)
            {
                for (int j = 0; j < col; j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        continue;
                    }
                    else if (i != row - 1 && j == 0)
                    {
                        matrix[i, j] = 2 * matrix[i + 1, j];
                    }
                    else
                    {
                        matrix[i, j] = 2 * matrix[i, j - 1];
                    }
                }
            }
            int PositionRow = row - 1;
            int PositionCol = 0;
            BigInteger sum = matrix[PositionRow, PositionCol];
            matrix[PositionRow, PositionCol] = 0;
            for (int i = 0; i < moveCount; i++)
            {
                int nextRow = (int)(movesArr[i] / coeff);
                int nextCol = (int)(movesArr[i] % coeff);
                if (nextCol > PositionCol)
                {
                    int moves = nextCol - PositionCol;
                    for (int j = 0; j < moves; j++)
                    {
                        if (matrix[PositionRow, PositionCol + 1] != 0)
                        {
                            sum += matrix[PositionRow, PositionCol + 1];
                            matrix[PositionRow, PositionCol + 1] = 0;
                        }
                        PositionCol++;
                    }
                }
                else
                {
                    int moves = PositionCol - nextCol;
                    for (int j = 0; j < moves; j++)
                    {
                        if (matrix[PositionRow, PositionCol - 1] != 0)
                        {
                            sum += matrix[PositionRow, PositionCol - 1];
                            matrix[PositionRow, PositionCol - 1] = 0;
                        }
                        PositionCol--;
                    }
                }
                if (nextRow>PositionRow)
                {
                    int moves = nextRow - PositionRow;
                    for (int j = 0; j < moves; j++)
                    {
                        if (matrix[PositionRow+1,PositionCol]!=0)
                        {
                            sum += matrix[PositionRow + 1, PositionCol];
                            matrix[PositionRow + 1, PositionCol] = 0;
                        }
                        PositionRow++;
                    }
                }else
                {
                    int moves = PositionRow - nextRow;
                    for (int j = 0; j <moves; j++)
                    {
                        if (matrix[PositionRow-1,PositionCol]!=0)
                        {
                            sum += matrix[PositionRow - 1, PositionCol];
                            matrix[PositionRow - 1, PositionCol] = 0;
                        }
                        PositionRow--;
                    }
                }
            }
            Console.WriteLine(sum);
        }
    }
}
