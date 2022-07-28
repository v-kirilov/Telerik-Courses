using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQify.Tasks
{
    public static class Task05
    {
        //Task 05 - Find if any person's first name is Terry

        public static bool Execute(List<Person> people)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].FirstName == "Terry")
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ExecuteWithLINQ(List<Person> people)
        {
            return people.Any(x => x.FirstName == "Terry");
        }
    }
}
