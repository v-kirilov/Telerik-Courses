using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO.Users
{
    public class UserCommentReactionDto
    {
        public UserCommentReactionDto(CommentReaction reaction)
        {
            this.Id = reaction.Id;
            this.Author = reaction.User.Username;
            this.Reaction = reaction.Reaction.ToString();
        }
        public int Id { get; set; }
        public string Author { get; set; }
        public string Reaction { get; set; }
    }
}
