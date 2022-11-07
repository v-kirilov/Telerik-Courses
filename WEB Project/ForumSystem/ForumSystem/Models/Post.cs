using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]

        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<PostReaction> Reactions { get; set; } = new List<PostReaction>();

    }
}
