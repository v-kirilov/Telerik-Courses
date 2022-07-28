using System;

namespace GenericDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            //Func - accepts up to 16 params and returns 1 param
            Func<int, int, int> sum = (num1, num2) => num1 + num2;
            var res = sum(4, 5); // 9
            var res1 = sum(123, 151231); // 151354
            var res2 = sum(2, 12); // 14


            //Action - accepts up to 16 params and returns void
            Action<string> print = str => Console.WriteLine(str);
            print("James Baxter");
            print("Justin Bieber");


            //Predicate - accepts 1 param and returns bool
            Predicate<int> largerThanFive = num => num > 5;
            largerThanFive(6); // true
            largerThanFive(4); // false
        }
    }
}
