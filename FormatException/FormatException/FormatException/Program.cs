using System;

namespace Format

{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int age = int.Parse(Console.ReadLine());
                Console.WriteLine($"Your age is {age}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"{ex.Message} Please, enter a valid number.");
            }
        }
    }
}
