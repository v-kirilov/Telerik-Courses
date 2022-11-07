using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Models.DTO
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(500, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(4, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string CommentContent { get; set; }

        public string Author { get; set; }
        public int PostId { get; set; }
        public ICollection<CommentReactionDto> CommentReactions { get; set; }
        
    }
}
