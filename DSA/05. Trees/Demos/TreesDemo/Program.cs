using System;
using System.Collections.Generic;

namespace TreesDemo
{
	class Program
	{
		static void Main()
		{
			/*
						   the tree:
							  8
							 / \
							66  5
							   / \
							  3   9
		   */

			TreeNode<int> root = new TreeNode<int>(
				value: 8
			);

			TreeNode<int> anotherNode = new TreeNode<int>(
				value: 5,
				children: new List<TreeNode<int>>()
							{
								new TreeNode<int>(3),
								new TreeNode<int>(9)
							}
			);

			root.children = new List<TreeNode<int>>()
								{
									new TreeNode<int>(value: 66),
									anotherNode
								};

			//root.bfs();
			//root.dfs();

			BinaryTreeNode<int> binaryTreeRoot = new BinaryTreeNode<int>(
				value: 8
			);

			BinaryTreeNode<int> binaryTreeAnotherNode = new BinaryTreeNode<int>(
				value: 5,
				left: new BinaryTreeNode<int>(3),
				right: new BinaryTreeNode<int>(9)
			);

			binaryTreeRoot.left = new BinaryTreeNode<int>(
				value: 66
			);

			binaryTreeRoot.right = binaryTreeAnotherNode;

			//binaryTreeRoot.bfs();

			Console.WriteLine("Preorder:");
			binaryTreeRoot.DepthFirstPreOrderTraversal();
			Console.WriteLine();
			Console.WriteLine("~~~");

			Console.WriteLine("Postorder:");
			binaryTreeRoot.DepthFirstPostOrderTraversal();
			Console.WriteLine();
			Console.WriteLine("~~~");

			Console.WriteLine("Inorder:");
			binaryTreeRoot.DepthFirstInOrderTraversal();
		}
	}
}
