using System;
using System.Collections.Generic;

namespace DSA.Linear
{
	class Program
	{
		static void Main()
		{
            Stack<int> stack = new Stack<int>();

            stack.Push(1); // O(1) - Constant Time Complexity
            stack.Push(2); // O(1) - Constant Time Complexity
            stack.Push(3); // O(1) - Constant Time Complexity
            stack.Push(4); // O(1) - Constant Time Complexity
            stack.Push(5); // O(1) - Constant Time Complexity

            //        5   <-- top of the stack
            //        4
            //        3
            //        2
            //        1

            int top = stack.Peek(); // O(1) - Constant Time Complexity
            Console.WriteLine($"Top of the stack: {top}");

            top = stack.Pop(); // O(1) - Constant Time Complexity
            Console.WriteLine($"Top of the stack: {top}");

            top = stack.Pop(); // O(1) - Constant Time Complexity
            Console.WriteLine($"Top of the stack: {top}");

            top = stack.Pop(); // O(1) - Constant Time Complexity
            Console.WriteLine($"Top of the stack: {top}");

            top = stack.Pop(); // O(1) - Constant Time Complexity
            Console.WriteLine($"Top of the stack: {top}");

            top = stack.Pop(); // O(1) - Constant Time Complexity
            Console.WriteLine($"Top of the stack: {top}");

            // WARNING!
            // Popping from an empty stack throws an exception.
            // Uncomment the lines below to test it.
            // top = stack.Pop(); // O(1) - Constant Time Complexity
            // Console.WriteLine($"Top of the stack: {top}");
        }
    }
}
