using System;
using System.Collections.Generic;

namespace TreesDemo
{
	public class BinaryTreeNode<T>
	{
		private T value;
		public  BinaryTreeNode<T> left;
		public BinaryTreeNode<T> right;

		public BinaryTreeNode(T value, BinaryTreeNode<T> left, BinaryTreeNode<T> right)
		{
			this.value = value;
			this.left = left;
			this.right = right;
		}

		public BinaryTreeNode(T value)
		{
			this.value = value;
		}

		public void BreadthFirstTraversal()
		{
			var queue = new Queue<BinaryTreeNode<T>>();
			queue.Enqueue(this);

			while (queue.Count != 0)
			{
				BinaryTreeNode<T> current = queue.Dequeue();

				Console.Write($" {current.value} ");

				if (current.left != null)
				{
					queue.Enqueue(current.left);
				}
				if (current.right != null)
				{
					queue.Enqueue(current.right);
				}
			}
		}

		/*
         * The following 3 methods are almost idential.
         * Pay attention where the Console.Write occurs!
         */

		public void DepthFirstPreOrderTraversal()
		{
			Console.Write($" {value} ");

			if (left != null)
			{
				left.DepthFirstPreOrderTraversal();
			}

			if (right != null)
			{
				right.DepthFirstPreOrderTraversal();
			}
		}

		public void DepthFirstInOrderTraversal()
		{
			if (left != null)
			{
				left.DepthFirstInOrderTraversal();
			}

			Console.Write($" {value} ");

			if (right != null)
			{
				right.DepthFirstInOrderTraversal();
			}
		}

		public void DepthFirstPostOrderTraversal()
		{
			if (left != null)
			{
				left.DepthFirstPostOrderTraversal();
			}

			if (right != null)
			{
				right.DepthFirstPostOrderTraversal();
			}

			Console.Write($" {value} ");
		}
	}
}
