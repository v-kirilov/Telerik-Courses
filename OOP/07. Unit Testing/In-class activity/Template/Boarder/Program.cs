using Boarder.Loggers;
using Boarder.Models;
using System;

namespace Boarder
{
    class Program
    {
        static void Main(string[] args)
        {
            var tomorrow = DateTime.Now.AddDays(1);
            BoardItem task = new Task("Write unit tests", "Pesho", tomorrow);
            BoardItem issue = new Issue("Review tests", "Someone must review Pesho's tests.", tomorrow);

            Console.WriteLine(task.ViewInfo());
            Console.WriteLine(issue.ViewInfo());
        }
    }
}
