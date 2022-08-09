using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Story;

namespace Task_Management.Commands
{
    public class ChangeStoryCommand : BaseCommand
    {
        public ChangeStoryCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (this.CommandParameters.Count != 3)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 3, Received: {this.CommandParameters.Count}");
            }

            // [0] - int StoryId
            // [1] - string type of change - Priority, Size, Status
            // [2] - string value

            int storyId = this.ParseIntParameter(this.CommandParameters[0], "StoryID");
            IStory story = base.Repository.FindStoryById(storyId);

            string typeOfChange = this.CommandParameters[1];
            string value = this.CommandParameters[2];

            return ChangeStoryByGivenType(story, typeOfChange, value);
        }

        public string ChangeStoryByGivenType(IStory story, string typeOfChange, string value)
        {
            switch (typeOfChange.ToLower())
            {
                case "priority":
                    Priority newStoryPriority = this.ParseStoryPriorityParameter(value, "Priority");
                    var oldPriority = story.Priority;
                    story.Priority = newStoryPriority;
                    if (story.Priority == oldPriority)
                    {
                        return $"Story with ID: {story.Id} is already: {story.Priority}";
                    }
                    return $"Changed the priority of status with ID: {story.Id} from {oldPriority} to {story.Priority}";

                case "size":
                    Size newStorySize = this.ParseSizeParameter(value, "Size");
                    var oldSize = story.Size;
                    story.Size = newStorySize;
                    if (story.Size == oldSize)
                    {
                        return $"Story with ID: {story.Id} is already: {story.Size}";
                    }
                    return $"Changed the size of story with ID: {story.Id} from {oldSize} to {story.Size}";

                case "status":
                    Status newStoryStatus = this.ParseStoryStatusParameter(value, "Status");
                    var oldStatus = story.Status;
                    story.Status = newStoryStatus;
                    if (story.Status == oldStatus)
                    {
                        return $"Story with ID: {story.Id} is already: {story.Status}";
                    }
                    return $"Changed the status of story with ID: {story.Id} from {oldStatus} to {story.Status}";


                default:
                    throw new EntityNotFoundException("Wrong type of change. Should be either Priority, Size or Status");
            }
        }
    }
}
