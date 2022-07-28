using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task06
    {
        //Task 06 - Find the count of people who's first name is longer than 6 symbols and their eye color is brown

        public static int Execute(List<Person> people)
        {
            int count = 0;
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].FirstName.Length > 6 && people[i].EyeColor == "brown")
                {
                    count++;
                }
            }
            return count;
        }

        public static int ExecuteWithLINQ(List<Person> people)
        {
            return people.Count(x => x.FirstName.Length > 6 && x.EyeColor == "brown");
        }
    }
}
