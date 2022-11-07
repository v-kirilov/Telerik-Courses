using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO.Users
{
    public class UserSpecificPostDto
    {
        public UserSpecificPostDto(Post post)
        {
            this.Id = post.Id;
            this.Title = post.Title;
            this.Content = post.Content;
            this.Author = post.User.Username;
            this.reactions = post.Reactions.Select(r => new PostReactionDto(r)).ToList();
            this.comments = post.Comments.Select(c => new PostAllCommentsDto(c)).ToList();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public List<PostReactionDto> reactions { get; set; }
        public List<PostAllCommentsDto> comments { get; set; }
    }
}
