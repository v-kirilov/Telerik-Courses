using System;

namespace P12_ChangePi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string output = string.Empty;
            Console.WriteLine(Count(input,output));
        }

        private static string Count(string input,string output)
        {
            if (input.Length == 1)
            {
                return output + input;
            }
            if (input[0] == 'p')
            {

                if (input[1] == 'i')
                {
                    if (input.Length == 2)
                    {
                        output = output + "3.14";
                        return output;
                    }
                    output = output + "3.14";
                    return Count(input.Substring(2),output);

                }
            }
            output = output + input[0];
            return Count(input.Substring(1),output);
        }
    }
}
