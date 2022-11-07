using System;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parent p = new Child();
            p.Print();
        }
    }

    class Parent
    {
        public virtual void Print()
        {
            Console.WriteLine("I am parent");
        }
    }
    class Child : Parent
    {
        public override void Print()
        {
            Console.WriteLine("I am child");
        }
    }
}
