using System;
using System.Linq;

namespace SelectMany
{
    class Program
    {
        static void Main(string[] args)
        {
            //Projects each element of a sequence to an IEnumerable<T> and flattens the resulting sequences into one sequence.

            string[] array = { "cat", "dog", "elepehant", "turtle" };

            // Convert each string in the string array to a character array.
            // ... Then combine those character arrays into one.
            var result = array.SelectMany(element => element.ToCharArray());

            // Display letters.
            foreach (char letter in result)
            {
                Console.WriteLine(letter);
            }
        }
    }
}
