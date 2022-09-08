using System;
using System.Collections.Generic;

namespace DSA.Linear
{
	class Program
	{
        static void Main()
        {
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(1); // O(1) - Constant Time Complexity
            queue.Enqueue(2); // O(1) - Constant Time Complexity
            queue.Enqueue(3); // O(1) - Constant Time Complexity
            queue.Enqueue(4); // O(1) - Constant Time Complexity
            queue.Enqueue(5); // O(1) - Constant Time Complexity

            //        1 2 3 4 5
            //        ^
            //        first element in the queue

            int first = queue.Peek(); // O(1) - Constant Time Complexity
            Console.WriteLine($"First in the queue: {first}");

            first = queue.Dequeue(); // O(1) - Constant Time Complexity
            Console.WriteLine($"First in the queue: {first}");

            first = queue.Dequeue(); // O(1) - Constant Time Complexity
            Console.WriteLine($"First in the queue: {first}");

            first = queue.Dequeue(); // O(1) - Constant Time Complexity
            Console.WriteLine($"First in the queue: {first}");

            first = queue.Dequeue(); // O(1) - Constant Time Complexity
            Console.WriteLine($"First in the queue: {first}");

            first = queue.Dequeue(); // O(1) - Constant Time Complexity
            Console.WriteLine($"First in the queue: {first}");

			// WARNING!
			// Dequeueing from an empty queue throws an exception.
			// Uncomment the lines below to test it.
			first = queue.Dequeue(); // O(1) - Constant Time Complexity
			Console.WriteLine($"First in the queue: {first}");
		}
    }
}
