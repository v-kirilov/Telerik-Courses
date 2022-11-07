using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO.Users
{
    public class PostsCommentUserDto
    {
        public PostsCommentUserDto(Post post)
        {
            this.Title = post.Title;
            this.Content = post.Content;
            this.Author = post.User.Username;
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}
