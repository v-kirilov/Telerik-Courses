using ForumSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Models.DTO
{
    public class PostReactionDto
    {
        public PostReactionDto()
        {

        }
        public PostReactionDto(PostReaction postReaction)
        {
            this.Id = postReaction.Id;
            this.Author = postReaction.User.Username;
            this.Reaction = postReaction.Reaction.ToString();
        }
        public int Id { get; set; }

        public string Author { get; set; }
        [Required]
        public string Reaction { get; set; }
    }
}
