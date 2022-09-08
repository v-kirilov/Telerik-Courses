using System;

namespace LogarithmicComplexity
{
	class Program
	{
		static void Main(string[] args)
		{

		}

        // You are given a rope that is n meters long.
        // How many times you should fold it to become less than 1 meter?
        static int FoldingRope(int n)
        {
            int foldingCounter = 0;

            while (n > 1)
            {
                foldingCounter++;
                n /= 2;
            }

            return foldingCounter;
        }
    }
}
