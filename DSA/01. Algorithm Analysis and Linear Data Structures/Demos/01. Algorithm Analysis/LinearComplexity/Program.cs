using System;

namespace LinearComplexity
{
	class Program
	{
		static void Main(string[] args)
		{

		}

		static int Sum(int[] array)
		{
			int sum = 0;

			foreach (int element in array)
			{
				sum += element;
			}

			return sum;
		}

		static int SumEvenIndexes(int[] array)
		{
			int sum = 0;

			for (int i = 0; i < array.Length; i += 2)
			{
				sum += array[i];
			}

			return sum;
		}

		static int SumEvenNumbers(int[] array)
		{
			int sum = 0;

			foreach (int element in array)
			{
				if (element % 2 == 0)
				{
					sum += element;
				}
			}

			return sum;
		}

		static int Max(int[] array)
		{
			int max = array[0];

			foreach (int element in array)
			{
				max = Math.Max(max, element);
			}

			return max;
		}
	}
}
