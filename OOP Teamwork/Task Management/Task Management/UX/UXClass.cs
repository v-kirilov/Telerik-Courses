using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.UX
{
    public static class UXClass
    {
        public static string welcomText = @"
 _    _      _                              _        
| |  | |    | |                            | |       
| |  | | ___| | ___ ___  _ __ ___   ___    | |_ ___  
| |/\| |/ _ \ |/ __/ _ \| '_ ` _ \ / _ \   | __/ _ \ 
\  /\  /  __/ | (_| (_) | | | | | |  __/   | || (_) |
 \/  \/ \___|_|\___\___/|_| |_| |_|\___|    \__\___/ 

";
        public static string taskManager = @"
     _____         _   ___  ___                                                  _      _____  _____  _____  _____ 
    |_   _|       | |  |  \/  |                                                 | |    |____ ||  _  ||  _  ||  _  |
      | | __ _ ___| | _| .  . | __ _ _ __   __ _  __ _  ___ _ __ ___   ___ _ __ | |_       / /| |/' || |/' || |/' |
      | |/ _` / __| |/ / |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '_ ` _ \ / _ \ '_ \| __|      \ \|  /| ||  /| ||  /| |
      | | (_| \__ \   <| |  | | (_| | | | | (_| | (_| |  __/ | | | | |  __/ | | | |_   .___/ /\ |_/ /\ |_/ /\ |_/ /
      \_/\__,_|___/_|\_\_|  |_/\__,_|_| |_|\__,_|\__, |\___|_| |_| |_|\___|_| |_|\__|  \____/  \___/  \___/  \___/ 
                                                  __/ |                                                            
                                                 |___/                                                             
";
        public static string welcomeMessage = @"Hello and Welcome to your favourite Task Management System.";
        public static string helpMessage = @"If you need help, enter command [help].";
        public static string exitMessage = @"If you want to exit, enter command [exit].";

        public static void WelcomeMessage()
        {
            Console.Title = "Task Management 3000";
            StringBuilder sb = new StringBuilder();

            sb.Append(welcomText);
            sb.AppendLine(taskManager);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(sb);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(helpMessage);
            Console.WriteLine(exitMessage);
            Console.WriteLine();
            Console.WriteLine("Please enter a command:");
            Console.ForegroundColor = ConsoleColor.Gray;

        }
    }
}
