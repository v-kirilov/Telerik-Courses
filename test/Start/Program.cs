using Start.model;
using System;

namespace Start
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Child child1 = new Child("Pesho", 20, "Plebo");
            child1.AddGrade(5);
            child1.AddGrade(6);
            child1.AddGrade(6);

                Console.WriteLine(child1.Name);
                Console.WriteLine(child1.Age);
                Console.WriteLine(child1.Nickname);
            foreach (var grade in child1.Grades)
            {
                Console.WriteLine(grade);
            }
        }
    }
}
