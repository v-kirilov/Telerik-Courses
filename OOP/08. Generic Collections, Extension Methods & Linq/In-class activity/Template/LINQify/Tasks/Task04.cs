using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task04
    {
        //Task 04 - Find if all people are older than 18

        public static bool Execute(List<Person> people)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].Age <= 18)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ExecuteWithLINQ(List<Person> people)
        {
            return people.Any(x => x.Age < 18);
        }
    }
}
