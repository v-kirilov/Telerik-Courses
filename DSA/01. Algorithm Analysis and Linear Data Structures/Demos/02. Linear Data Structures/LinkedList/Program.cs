using System;
using System.Collections.Generic;

namespace DSA.Linear
{
	class Program
	{
		static void Main(string[] args)
		{
			LinkedList<int> linkedList = new LinkedList<int>();

			linkedList.AddFirst(1); // O(1) - Constant Time Complexity
			Console.WriteLine(string.Join(", ", linkedList)); // O(n) - Linear Time Complexity

			linkedList.AddLast(2);  // O(1) - Constant Time Complexity
			Console.WriteLine(string.Join(", ", linkedList)); // O(n) - Linear Time Complexity

			linkedList.AddFirst(0); // O(1) - Constant Time Complexity
			Console.WriteLine(string.Join(", ", linkedList)); // O(n) - Linear Time Complexity

			linkedList.AddLast(4);  // O(1) - Constant Time Complexity
			Console.WriteLine(string.Join(", ", linkedList)); // O(n) - Linear Time Complexity

			LinkedListNode<int> node = linkedList.Find(4); // O(n) - Linear Time Complexity
			linkedList.AddBefore(node, 3); // O(1) - Constant Time Complexity
			Console.WriteLine(string.Join(", ", linkedList)); // O(n) - Linear Time Complexity
		}
	}
}
