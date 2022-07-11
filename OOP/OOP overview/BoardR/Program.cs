using System;
using System.Threading;

namespace BoardR
{
    internal class Program
    {
        static void Main(string[] args)
        {



            var issue = new Issue("App flow tests?", "We need to test the App!", DateTime.Now.AddDays(1));
            issue.AdvanceStatus();
            issue.DueDate = issue.DueDate.AddDays(1);
            Console.WriteLine(issue.ViewHistory());

            Console.WriteLine("-------------");

            var tomorrow = DateTime.Now.AddDays(1);
            var newIssue = new Issue("App flow tests?", "We need to test the App!", tomorrow);
            var task = new Task("Test the application flow", "Pesho", tomorrow);

            Board.AddItem(newIssue);
            Board.AddItem(task);
            Console.WriteLine(Board.TotalItems); // 2




        }
    }
}
