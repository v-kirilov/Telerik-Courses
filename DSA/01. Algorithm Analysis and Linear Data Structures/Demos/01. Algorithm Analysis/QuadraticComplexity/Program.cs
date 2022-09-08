using System;

namespace QuadraticComplexity
{
	class Program
	{
		static void Main(string[] args)
		{

		}

		static void FindPairs(int[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[i] == array[j] && i != j)
					{
						Console.WriteLine($"({i}, {j})");
					}
				}
			}
		}
	}
}
