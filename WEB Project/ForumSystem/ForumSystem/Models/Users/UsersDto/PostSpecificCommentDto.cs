using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO.Users
{
    public class PostSpecificCommentDto
    {
        public PostSpecificCommentDto(Comment comment)
        {
            this.Id = comment.Id;
            this.Content = comment.CommentContent;
            this.Author = comment.User.Username;
            this.Reactions = comment.CommentReactions.Select(c => new CommentReactionDto(c)).ToList();
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public List<CommentReactionDto> Reactions { get; set; }
    }
}
