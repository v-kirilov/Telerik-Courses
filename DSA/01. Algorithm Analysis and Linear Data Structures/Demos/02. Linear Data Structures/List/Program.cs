using System;
using System.Collections.Generic;

namespace DSA.Linear
{
	class Program
	{
		static void Main()
		{
			int initialCapacity = 4;
			List<int> list = new List<int>(initialCapacity);

			// Add() adds at the end
			list.Add(1); // O(1) - Constant Time Complexity
			list.Add(2); // O(1) - Constant Time Complexity
			list.Add(3); // O(1) - Constant Time Complexity
			list.Add(4); // O(1) - Constant Time Complexity
			list.Add(5); // O(n) - Linear Time Complexity. The internal array is full and has to be resized.
			list.Add(6); // O(1) - Constant Time Complexity

			int index = 0;
			int itemToInsert = 0;
			list.Insert(index, itemToInsert); // O(n) - Linear Time Complexity

			Console.WriteLine(list[4]); // O(1) - Constant Time Complexity

			int itemToRemove = 1;
			list.Remove(itemToRemove); // O(n) - Linear Time Complexity

			itemToRemove = 6;
			list.Remove(itemToRemove); // O(n) - Linear Time Complexity

			int firstIndex = 0;
			list.RemoveAt(firstIndex); // O(n) - Linear Time Complexity

			int lastIndex = list.Count - 1;
			list.RemoveAt(lastIndex); // O(1) - Constant Time Complexity

			Console.WriteLine(list.Contains(4)); // O(n) - Linear Time Complexity
		}
	}
}
