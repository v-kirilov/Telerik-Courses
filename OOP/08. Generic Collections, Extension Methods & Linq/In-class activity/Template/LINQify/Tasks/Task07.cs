using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task07
    {
        //Task 07 - Find the first person with brown eyes and return it

        public static Person Execute(List<Person> people)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].EyeColor == "brown")
                {
                    return people[i];
                }
            }

            return null;
        }

        public static Person ExecuteWithLINQ(List<Person> people)
        {
            return people.First(x=>x.EyeColor=="brown");
        }
    }
}
