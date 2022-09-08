using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        HashSet<int> evenNumbers = new HashSet<int>();
        HashSet<int> oddNumbers = new HashSet<int>();

        for (int i = 0; i < 5; i++)
        {
            // Add even numbers only
            evenNumbers.Add(i * 2);

            // Add odd numbers only
            oddNumbers.Add((i * 2) + 1);
        }

        Console.Write($"evenNumbers has {evenNumbers.Count} items: ");
        Console.WriteLine($"{{ {string.Join(" ", evenNumbers)} }}");

        Console.Write($"oddNumbers contains {oddNumbers.Count} items: ");
        Console.WriteLine($"{{ {string.Join(" ", oddNumbers)} }}");

        // Create a new HashSet populated with even numbers
        HashSet<int> allNumbers = new HashSet<int>(evenNumbers);
        // Add the odd numbers using UnionWith
        allNumbers.UnionWith(oddNumbers);
        Console.Write($"allNumbers contains {allNumbers.Count} items: ");
        Console.WriteLine($"{{ {string.Join(" ", allNumbers)} }}");
    }
}
