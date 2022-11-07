using System.Collections.Generic;

namespace ForumSystem.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public List<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();
         

    }
}
