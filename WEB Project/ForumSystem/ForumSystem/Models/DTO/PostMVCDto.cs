using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Models.DTO
{
    public class PostMVCDto
    {

        public int Id { get; set; }        
        public int UserId { get; set; }     

        public string User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        
        public List<PostReactionDto> reactions = new List<PostReactionDto>();       

        public List<CommentDto> comments = new List<CommentDto>();

        public CommentDto Comment { get; set; }

        public PostReactionDto Reaction { get; set; }
    }
}
