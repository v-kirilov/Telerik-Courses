using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Models.DTO
{
    public class PostDto
    {
        [JsonProperty(Order = 1)]

        public int Id { get; set; }
        [JsonProperty(Order = 2)]
        //[Required]
        public int UserId { get; set; }
        [JsonProperty(Order = 3)]

        public string User { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(64, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(16, ErrorMessage = "The {0} field must be at least {1} character.")]

        [JsonProperty(Order = 4)]

        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(8192, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(32, ErrorMessage = "The {0} field must be at least {1} character.")]

        [JsonProperty(Order = 5)]

        public string Content { get; set; }
        [JsonProperty(Order = 6)]
        
        public List<PostReactionDto> reactions = new List<PostReactionDto>();

        [JsonProperty(Order = 7)]

        public List<CommentDto> comments = new List<CommentDto>();
    }
}
