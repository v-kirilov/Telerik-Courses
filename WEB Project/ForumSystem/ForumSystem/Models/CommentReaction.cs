using ForumSystem.Models.Enums;

namespace ForumSystem.Models
{
    public class CommentReaction
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

        public Reactions Reaction { get; set; }
    }
}
