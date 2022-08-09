using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Exceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Story;

namespace Task_Management.Core
{
    public class Repository : IRepository
    {
        private int currentId;

        private readonly List<ITeam> teams = new List<ITeam>();
        private readonly List<IMember> members = new List<IMember>();

        private readonly IList<IBug> bugs = new List<IBug>();
        private readonly IList<IStory> stories = new List<IStory>();
        private readonly IList<IFeedback> feedbacks = new List<IFeedback>();

        public Repository()
        {
            this.currentId = 0;
        }
        public List<ITeam> Teams
        {
            get
            {
                return new List<ITeam>(this.teams);
            }
        }
        public List<IMember> Members
        {
            get
            {
                return new List<IMember>(this.members);
            }
        }

        public IList<IBug> Bugs
        {
            get
            {
                return new List<IBug>(this.bugs);
            }
        }

        public IList<IStory> Stories
        {
            get
            {
                return new List<IStory>(this.stories);
            }
        }

        public IList<IFeedback> Feedbacks
        {
            get
            {
                return new List<IFeedback>(this.feedbacks);
            }
        }

        public ITeam CreateTeam(string name)
        {
            if (this.teams.Any(x => x.Name == name) == true) //==true is not necessary but added for readability
            {
                throw new InvalidUserInputException($"Team with name:{name} already exists! Please choose a different name.");
            }
            var team = new Team(name);
            this.teams.Add(team);
            return team;

        }
        public IMember CreateMember(string name)
        {
            if (this.members.Any(x => x.Name == name) == true) //==true is not necessary but added for readability
            {
                throw new InvalidUserInputException($"Member with name:[{name}] already exists! Please choose a different name.");
            }
            var member = new Member(name);
            this.members.Add(member);
            return member;
        }

        public IBug CreateBug(string title, string description, string steps, Models.Enums.Bug.Priority priority, Severity severity, IMember assignee)
        {
            IBug bug = new Bug(this.currentId + 1, title, description, steps, priority, severity, assignee);
            this.bugs.Add(bug);
            
            this.currentId++;
            return bug;
        }

        public IStory CreateStory(string title, string description, Models.Enums.Story.Priority priority, Size size, IMember assignee)
        {
            IStory story = new Story(this.currentId + 1, title, description, priority, size, assignee);
            this.stories.Add(story);

            this.currentId++;
            return story;
        }

        public IFeedback CreateFeedback(string title, string description, int rating)
        {
            IFeedback feedback = new Feedback(this.currentId + 1, title, description, rating);
            this.feedbacks.Add(feedback);

            this.currentId++;
            return feedback;
        }

        public ITeam FindTeamByName(string teamName)
        {
            if (this.teams.Any(x => x.Name == teamName))
            {
                return this.teams.First(team => team.Name == teamName);
            }

            throw new EntityNotFoundException($"There is no team with name: {teamName}");
        }

        public IMember FindMemberByName(string name)
        {
            if (this.members.Any(x => x.Name == name))
            {
                return this.members.First(m => m.Name == name);
            }

            throw new EntityNotFoundException($"There is no member with name: {name}");
        }

        public IBoard FindBoardByName(string name)
        {
            if (this.teams.Any(x => x.Boards.Any(x => x.Name == name)))
            {
                ITeam teamWithTheBoardInit = this.teams.First(x => x.Boards.Any(x => x.Name == name));
                return teamWithTheBoardInit.Boards.First(x => x.Name == name);
            }

            throw new EntityNotFoundException($"There is no board with name: {name}");
        }

        public IBug FindBugById(int bugId)
        {
            if (this.bugs.Any(x => x.Id == bugId))
            {
                return this.bugs.First(b => b.Id == bugId);
            }
            
            throw new EntityNotFoundException($"There is no bug with ID: {bugId}");
        }

        public IStory FindStoryById(int storyId)
        {
            if (this.stories.Any(x => x.Id == storyId))
            {
                return this.stories.First(s => s.Id == storyId);
            }

            throw new EntityNotFoundException($"There is no story with ID: {storyId}");
        }

        public IFeedback FindFeedbackById(int feedbackId)
        {
            if (this.feedbacks.Any(x => x.Id == feedbackId))
            {
                return this.feedbacks.First(f => f.Id == feedbackId);
            }

            throw new EntityNotFoundException($"There is no feedback with ID: {feedbackId}");
        }

        public ITask FindTaskById(int taskId)
        {
            if (this.bugs.Any(x => x.Id == taskId))
            {
                return this.bugs.First(b => b.Id == taskId);
            }
            else if (this.stories.Any(x => x.Id == taskId))
            {
                return this.stories.First(s => s.Id == taskId);
            }
            else if (this.feedbacks.Any(x => x.Id == taskId))
            {
                return this.feedbacks.First(f => f.Id == taskId);
            }
            else
            {
                throw new EntityNotFoundException($"There is no task with ID: {taskId}");
            }
        }
    }
}
