using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task03
    {
        //Task 03 - Filter the people who are over 30 and then sum their age

        public static int Execute(List<Person> people)
        {
            int totalAge = 0;

            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].Age > 30)
                {
                    totalAge += people[i].Age;
                }
            }

            return totalAge;
        }

        public static int ExecuteWithLINQ(List<Person> people)
        {
           return people
                .Where(x => x.Age > 30)
                .Sum(x => x.Age);
        }
    }
}
