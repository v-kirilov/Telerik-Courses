using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Story;

namespace Task_Management.Core.Contracts
{
    public interface IRepository
    {
        List<ITeam> Teams { get; }
        List<IMember> Members { get; }
        
        IList<IBug> Bugs { get; }
        IList<IStory> Stories { get; }
        IList<IFeedback> Feedbacks { get; }

        public ITeam CreateTeam(string name);
        public IMember CreateMember(string name);

        public IBug CreateBug(string title, string description, string steps, Models.Enums.Bug.Priority priority, Severity severity, IMember assignee);
        public IStory CreateStory(string title, string description, Models.Enums.Story.Priority priority, Size size, IMember assignee);
        public IFeedback CreateFeedback(string title, string description, int rating);
        public IBug FindBugById(int bugId);
        public IStory FindStoryById(int storyId);
        public IFeedback FindFeedbackById(int feedbackId);
        public ITask FindTaskById(int taskId);
        public IMember FindMemberByName(string name);
        public IBoard FindBoardByName(string name);
        public ITeam FindTeamByName(string teamName);

       
    }
}
