﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creates a Dictionary<TKey,TValue> from an IEnumerable<T>.

            // Example integer array.
            int[] values = new int[] { 1, 3, 5, 7 };

            // First argument is the key, second the value.
            Dictionary<int, bool> dictionary =
                values.ToDictionary(v => v, v => true);

            // Display all keys and values.
            foreach (KeyValuePair<int, bool> pair in dictionary)
            {
                Console.WriteLine(pair);
            }
        }
    }
}
