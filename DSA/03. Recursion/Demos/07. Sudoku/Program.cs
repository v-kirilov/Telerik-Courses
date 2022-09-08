using System;

namespace _07._Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] table =
            {
                {0,4,0, 1,0,0, 0,0,2},
                {6,0,0, 0,0,4, 0,0,0},
                {0,0,5, 0,0,3, 4,1,0},

                {4,0,0, 5,0,0, 1,0,0},
                {0,0,8, 7,0,1, 0,0,0},
                {7,0,0, 0,8,0, 5,4,3},

                {1,0,9, 3,0,5, 2,0,0},
                {0,0,2, 0,0,0, 6,3,0},
                {0,3,0, 9,6,0, 0,5,0}
                };

            if (!SolveSudoku(table))
            {
                Console.WriteLine("No solution");
            }

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    Console.Write(" " + table[i, j]);
                }
                Console.WriteLine();
            }
        }

        static bool SolveSudoku(int[,] table, int row = 0, int col = 0)
        {
            if (col == 9)
            {
                row++;
                col = 0;
                if (row == 9)
                {
                    return true;
                }
            }

            if (table[row, col] > 0)
            {
                return SolveSudoku(table, row, col + 1);
            }

            for (int i = 1; i <= 9; ++i)
            {
                bool isOk = true;

                for (int k = 0; k < 9; ++k)
                {
                    if (table[row, k] == i || table[k, col] == i
                        || table[row / 3 * 3 + k / 3, col / 3 * 3 + k % 3] == i)
                    {
                        isOk = false;
                        break;
                    }

                }

                if (!isOk) continue;
                table[row, col] = i;

                if (SolveSudoku(table, row, col + 1))
                {
                    return true;
                }

                table[row, col] = 0;
            }

            return false;
        }
    }
}
