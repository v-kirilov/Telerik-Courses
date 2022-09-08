using System;
using System.Linq;

namespace DSA
{
	public static class ArrayHelper
	{
		public static int[] DuplicateArray (int[] sourceArray)
		{
			int[] destinationArray = new int[sourceArray.Length];
			Array.Copy(sourceArray, destinationArray, sourceArray.Length);
			return destinationArray;
		}
		public static int[] GenerateRandomArray(int desiredLength = 100)
		{
			Random rnd = new Random();

			return Enumerable
				.Range(1, desiredLength)
				.Select(x => rnd.Next(0, int.MaxValue))
				.ToArray();
		}
	}
}
