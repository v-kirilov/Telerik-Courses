using System;

namespace TestWelcomeSigns
{
    internal class Program
    {
        static int windowWith = 120;
        static void Main(string[] args)
        {
            Console.Title = "Task_Management_3000";


            string text = @"
 _____         _     ___  ___                                                  _     _____  _____  _____  _____ 
|_   _|       | |    |  \/  |                                                 | |   |____ ||  _  ||  _  ||  _  |
  | | __ _ ___| | __ | .  . | __ _ _ __   __ _  __ _  ___ _ __ ___   ___ _ __ | |_      / /| |/' || |/' || |/' |
  | |/ _` / __| |/ / | |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '_ ` _ \ / _ \ '_ \| __|     \ \|  /| ||  /| ||  /| |
  | | (_| \__ \   <  | |  | | (_| | | | | (_| | (_| |  __/ | | | | |  __/ | | | |_  .___/ /\ |_/ /\ |_/ /\ |_/ /
  \_/\__,_|___/_|\_\ \_|  |_/\__,_|_| |_|\__,_|\__, |\___|_| |_| |_|\___|_| |_|\__| \____/  \___/  \___/  \___/ 
                                                __/ |                                                           
                                               |___/                                                            
 ";

            string welcomeText = @"Welcome to the Task Management System, if you need assistance please type /help.";

            Console.WindowWidth = windowWith;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(welcomeText);
            System.Threading.Thread.Sleep(10000);


           

        }
    }
}
