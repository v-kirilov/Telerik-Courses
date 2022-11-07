using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO.Users
{
    public class PostAllCommentsDto
    {
        public PostAllCommentsDto(Comment comment)
        {
            this.Content = comment.CommentContent;
            this.Author = comment.User.Username;
        }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}
