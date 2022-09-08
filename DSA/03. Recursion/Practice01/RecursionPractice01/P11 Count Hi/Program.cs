using System;

namespace P11_Count_Hi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Console.WriteLine(Count(input));
        }

        private static long Count(string input)
        {
            if (input.Length <= 1)
            {

                return ;
            }
            if (input[0] == 'h')
            {
                
                if (input[1] == 'i')
                {
                    if (input.Length==2)
                    {
                        return 1;
                    }
                    return 1 + Count(input.Substring(2));

                }
            }
            return Count(input.Substring(1));
        }
    }
}
