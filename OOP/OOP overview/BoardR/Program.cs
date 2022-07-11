using System;
using System.Threading;

namespace BoardR
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Task task = new Task("Test the application flow", "Pesho", DateTime.Now.AddDays(1));
            task.AdvanceStatus();
            task.AdvanceStatus();
            task.Assignee = "Gosho";
            Console.WriteLine(task.ViewHistory());





        }
    }
}
