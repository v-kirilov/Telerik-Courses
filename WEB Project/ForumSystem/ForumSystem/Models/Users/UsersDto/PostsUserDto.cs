using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO
{
    public class PostsUserDto
    {
        public PostsUserDto(Post post)
        {
            this.Id = post.Id;
            this.Title = post.Title;
            this.Content = post.Content;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
