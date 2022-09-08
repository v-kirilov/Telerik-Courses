using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string word = "12";
            word = word.Substring(1);
            word = word.Substring(1);
            Console.WriteLine(word);
        }
    }
}
