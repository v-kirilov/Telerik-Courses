using System;

namespace P04_Knight_Moves
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            int dim = int.Parse(Console.ReadLine());

            int[,] matrix = new int[dim, dim];
            matrix[0, 0] = 1;
            int knightRow = 0;
            int knightCol = 0;

            if (dim == 1)
            {
                matrix = new int[1, 1]
                    {
                    {1}
                };
                PrintMatrix(matrix);
                Environment.Exit(0);
            }
            if (dim == 2)
            {
                matrix = new int[2, 2]
                    {
                    {1,2 },
                    {3,4 }
                };
                PrintMatrix(matrix);
                Environment.Exit(0);
            }

            int counter = 2;
            int[] currentPlace = { -1, -1 };
            while (true)
            {
                currentPlace = FindFreeCell(matrix, knightRow, knightCol);
                if (currentPlace[0] != -1)
                {
                    matrix[currentPlace[0], currentPlace[1]] = counter;
                    knightRow = currentPlace[0];
                    knightCol = currentPlace[1];
                    counter++;
                }
                else
                {
                    currentPlace = FindEmptySpace(matrix);
                    if (currentPlace[0] != -1)
                    {
                        matrix[currentPlace[0], currentPlace[1]] = counter;
                        knightRow = currentPlace[0];
                        knightCol = currentPlace[1];
                        counter++;
                    }
                    else
                    {
                        PrintMatrix(matrix);
                        break;
                    }

                }

            }

        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write($"{matrix[row, col]} ");
                }
                Console.WriteLine();
            }
        }

        static int[] FindFreeCell(int[,] matrix, int knightRow, int knightCol)
        {
            int maxRow = matrix.GetLength(0);
            int maxCol = matrix.GetLength(1);
            if (knightRow - 2 >= 0 && knightRow - 2 < maxRow && knightCol - 1 >= 0&& knightCol - 1<maxCol && matrix[knightRow - 2, knightCol - 1] == 0)
            {
                //1
                return new int[] { knightRow - 2, knightCol - 1 };
            }
            else if (knightRow - 2 >= 0&& knightRow - 2<maxRow && knightCol + 1 >= 0&& knightCol + 1<maxCol && matrix[knightRow - 2, knightCol + 1] == 0)
            {
                //2
                return new int[] { knightRow - 2, knightCol + 1 };
            }
            else if (knightRow - 1 >= 0&& knightRow - 1<maxRow && knightCol - 2 >= 0&& knightCol - 2<maxCol && matrix[knightRow - 1, knightCol - 2] == 0)
            {
                //3
                return new int[] { knightRow - 1, knightCol - 2 };
            }
            else if (knightRow - 1 >= 0&& knightRow - 1<maxRow && knightCol + 2 >= 0&& knightCol + 2<maxCol && matrix[knightRow - 1, knightCol + 2] == 0)
            {
                //4
                return new int[] { knightRow - 1, knightCol + 2 };
            }
            else if (knightRow + 1 >= 0&& knightRow + 1<maxRow && knightCol - 2 >= 0&& knightCol - 2<maxCol && matrix[knightRow + 1, knightCol - 2] == 0)
            {
                //5
                return new int[] { knightRow + 1, knightCol - 2 };
            }
            else if (knightRow + 1 >= 0&& knightRow + 1<maxRow && knightCol + 2 >= 0 && knightCol + 2<maxCol && matrix[knightRow + 1, knightCol + 2] == 0)
            {
                //6
                return new int[] { knightRow + 1, knightCol + 2 };
            }
            else if (knightRow + 2 >= 0&& knightRow + 2<maxRow && knightCol - 1 >= 0 && knightCol - 1<maxCol && matrix[knightRow + 2, knightCol - 1] == 0)
            {
                //7
                return new int[] { knightRow + 2, knightCol - 1 };
            }
            else if (knightRow + 2 >= 0&& knightRow + 2<maxRow && knightCol + 1 >= 0&& knightCol + 1<maxCol && matrix[knightRow + 2, knightCol + 1] == 0)
            {
                //8
                return new int[] { knightRow + 2, knightCol + 1 };
            }
            else
            {
                return new int[] { -1, -1 };
            }
        }

        static int[] FindEmptySpace(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }
    }
}
