using System;

namespace ReverseString
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(ReverseString("kireleT"));
        }

        static string ReverseString(string text)
        {
            // Bottom case
            if (text.Length == 1)
            {
                return text;
            }

            return ReverseString(text.Substring(1)) + text[0];
        }
    }
}
