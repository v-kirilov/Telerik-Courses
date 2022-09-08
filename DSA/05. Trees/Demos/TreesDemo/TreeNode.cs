using System;
using System.Collections.Generic;

namespace TreesDemo
{
	public class TreeNode<T>
	{
		private T value;
		public List<TreeNode<T>> children;

		public TreeNode(T value)
		{
			this.value = value;
			this.children = new List<TreeNode<T>>();
		}

		public TreeNode(T value, List<TreeNode<T>> children)
		{
			this.value = value;
			this.children = children;
		}

		public void BreadthFirstTraversal()
		{
			var queue = new Queue<TreeNode<T>>();
			queue.Enqueue(this);

			while (queue.Count != 0)
			{
				TreeNode<T> current = queue.Dequeue();

				Console.Write($" {current.value} ");

				foreach (var child in current.children)
				{
					queue.Enqueue(child);
				}
			}
		}

		public void DepthFirstTraversal()
		{
			Console.Write($" {value} ");

			foreach (TreeNode<T> child in children)
			{
				child.DepthFirstTraversal();
			}
		}
	}
}
