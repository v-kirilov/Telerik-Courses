using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person { Name = "Ivan", Age = 10 };
            update(person);
            Console.WriteLine($"{person.Name} {person.Age}");

        }

        private static void update(Person person)
        {
            person.Name = "Ivan Ivanov";
            person.Age = 20;

            person = new Person();
            person.Age = 30;
            person.Name = "Ivan Ivanov Ivanov";

            Person viktor = new Person(); //Това съществува само тук.
            viktor.Name = "Viktor";
            viktor.Age = 30;
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
