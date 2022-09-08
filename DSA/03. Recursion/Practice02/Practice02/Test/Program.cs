using System;
using System.Collections.Generic;

namespace Test
{

    internal class Program
    {


        static void Main(string[] args)
        {

            int num =3;
            
            string[] input = { "0", "1","2","3" };
            Vector(input, num);

          



        }

        private static void Vector(string[] input, int num)
        {
            if (num==0)
            {
                return;
            }
            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(input[num]);
               Vector(input, num - 1);
                Console.WriteLine();
            }
            
        }
    }
}
