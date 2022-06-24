using System;
using System.Text;

namespace P04_Signal_from_Space
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            StringBuilder str= new StringBuilder();
            str.Append(input);
            for (int i = 0; i < str.Length - 1; i++)
            {
                string repeat = str[i].ToString();

                string nextRepeat = str[i+1].ToString();
                if (repeat == nextRepeat)
                {
                    str = str.Remove(i, 1);
                    i--;
                }

            }
            Console.WriteLine(str);
        }
    }
}
