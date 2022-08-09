using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;

namespace Task_Management.Commands
{
    public class HelpCommand : BaseCommand
    {
        public HelpCommand(IList<string> commandParameters, IRepository repository)
            : base(null, repository)
        {

        }
        public override string Execute()

        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("List of available commands:");
            sb.AppendLine("Command parameters are separated by '/'.");
            sb.AppendLine("1.Create a new Person - Input:([createmember] [name of member - must be unique])");
            sb.AppendLine("2.Show all people - Input:([showallpeople])");
            sb.AppendLine("3.Show person's activity - Input:([showmemberactivity] [name of member - must be unique])");
            sb.AppendLine("4.Create a new Team - Input:([createteam] [name of team - must be unique])");
            sb.AppendLine("5.Show all teams - Input:([showallteams])");
            sb.AppendLine("6.Show team's activity - Input:([showteamsactivity] [name of team - must be unique])");
            sb.AppendLine("7.Add person to a team - Input:([addmembertoteam] [name of team - must be unique] [name of member - must be unique])");
            sb.AppendLine("8.Show all team members - Input:([showteammembers] [name of team - must be unique])");
            sb.AppendLine("9.Create a new board in a team - Input:([createboard] [name of board - must be unique] [name of team to assign the board to])");
            sb.AppendLine("10.Show all team boards - Input:([showallteamboards] [name of team to show boards])");
            sb.AppendLine("11.Show boards activity - Input:([showboardactivity] [name of board to show activity])");
            sb.AppendLine("12.Create a new Bug in a board. - Input:([createbug] [title] [description] [steps] [priority] [severity] [assignee] [BoardName])");
            sb.AppendLine("13.Create a new Story in a board. - Input:([createstory] [title] [description] [priority] [size] [assignee] [BoardName])");
            sb.AppendLine("14.Create a new Feedback in a board. - Input:([createfeedback] [title] [description] [priority] [rating] [BoardName])");
            sb.AppendLine("15.Change the Priority/Severity/Status of a bug. - Input:([changebug] [BugId] [type of change - Priority, Severity, Status] [value])");
            sb.AppendLine("17.Change the Priority/Size/Status of a story. - Input:([changestory] [StoryId] [type of change - Priority, Severity, Status] [value])");
            sb.AppendLine("18.Change the Rating/Status of a feedback. - Input:([changefeedback] [FeedbackId] [type of change - Rating, Status] [value])");
            sb.AppendLine("19.Assign a task to a person. - Input:([assigntask] [taskId] [nameAssignee])");
            sb.AppendLine("20.Unassign a task to a person. - Input:([unassigntask] [taskId])");
            sb.AppendLine("21.Add comment to a task. - Input:([addcomment]  [taskId] [content] [nameAuthor])");

            
            return sb.ToString();

        }
    }
}
