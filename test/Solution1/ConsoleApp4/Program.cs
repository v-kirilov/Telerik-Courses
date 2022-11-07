using System;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var p = new Child();
        }
    }

    class Parent
    {
        public Parent()
        {
            Console.WriteLine("Parent");
        }
    }
    class Child : Parent
    {
       public Child()
        {
            Console.WriteLine("child");
        }
    }
}
