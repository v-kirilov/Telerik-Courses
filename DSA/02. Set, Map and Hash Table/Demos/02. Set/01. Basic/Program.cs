using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var set = new HashSet<int>();

        // .Add(item) method - adds an element; 
        set.Add(1);
        set.Add(2);
        set.Add(3);

        // no effect if the element exists
        set.Add(1);
        set.Add(2);

        // .Count property - returns the number of elements (like .Length for arrays)
        Console.WriteLine("Count: " + set.Count);

        // .Remove(item) method - removes an element. Returns true if something was deleted.
        Console.WriteLine("Deleted 2: " + set.Remove(2)); // True
        Console.WriteLine("Deleted 2: " + set.Remove(2)); // False

        // We have only two elements after removing 2
        Console.WriteLine("Count: " + set.Count);

        // .Contains(item)
        Console.WriteLine("Contains 3: " + set.Contains(3)); // True
        Console.WriteLine("Contains 5: " + set.Contains(5)); // False

        // The HashSet is iterable
        // 1. can used inside for-of
        foreach (var item in set)
        {
            Console.WriteLine(item);
        }

        // 2. can be spread with ... operator
        List<int> asList = set.ToList();
        Console.WriteLine($".ToList: {string.Join(",", asList)}");
    }
}
