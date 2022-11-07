using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Models.DTO
{
    public class CommentReactionDto
    {
        public CommentReactionDto()
        {

        }
        public CommentReactionDto(CommentReaction commentReaction)
        {
            this.Id = commentReaction.Id;
            this.Author = commentReaction.User.Username;
            this.Reaction = commentReaction.Reaction.ToString();
        }
        public int Id { get; set; }
        public string Author { get; set; }
        [Required]
        public string Reaction { get; set; }
    }
}
