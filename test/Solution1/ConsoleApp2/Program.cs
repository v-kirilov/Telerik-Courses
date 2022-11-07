using System;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str1 = "Academy";
            string str2 = str1;
            str1 = "Alpha";

            Console.WriteLine($"{str1} {str2}");

            ToUpper(str1, str2);

            Console.WriteLine($"{str1} {str2}");
        }

        private static void ToUpper(string str1, string str2)
        {
            str1=str1.ToUpper();
            str2=str2.ToUpper();

        }
    }
}
