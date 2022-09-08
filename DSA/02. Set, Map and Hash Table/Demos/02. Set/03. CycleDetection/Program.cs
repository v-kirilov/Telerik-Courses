using System;
using System.Collections.Generic;
using System.Linq;

namespace CycleDetection
{
	class Program
	{
		static void Main()
		{
			var node1 = new Node(1);
			var node2 = new Node(2);
			var node3 = new Node(3);

			node1.Next = node2;
			node2.Next = node3;
			// cycle
			node3.Next = node1;

			Console.WriteLine(HasCycle(node1));
		}

		static bool HasCycle(Node node)
		{
			var current = node;
			var set = new HashSet<Node>();
			while (current != null)
			{
				if (!set.Add(current))
					return true;

				current = current.Next;
			}
			return false;
		}
	}
}
