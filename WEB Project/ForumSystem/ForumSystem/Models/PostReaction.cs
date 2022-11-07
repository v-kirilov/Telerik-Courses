using ForumSystem.Models.Enums;

namespace ForumSystem.Models
{
    public class PostReaction
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public Reactions Reaction { get; set; }
    }
}
