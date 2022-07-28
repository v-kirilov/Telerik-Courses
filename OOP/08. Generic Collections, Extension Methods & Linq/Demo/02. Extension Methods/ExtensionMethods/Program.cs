using System;

namespace ExtensionMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

            // Call the method as if it were an instance method of string
            //The first parameter is not specified when calling it
            //Instead it stands for the extended type
            int wordCount = text.WordCount(); // 19
        }
    }
}
