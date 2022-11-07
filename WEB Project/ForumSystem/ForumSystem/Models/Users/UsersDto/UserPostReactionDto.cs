using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO.Users
{
    public class UserPostReactionDto
    {
        public UserPostReactionDto(PostReaction postReaction)
        {
            this.Id = postReaction.Id;
            this.Author = postReaction.User.Username;
            this.Reaction = postReaction.Reaction.ToString();
        }
        public int Id { get; set; }

        public string Author { get; set; }

        public string Reaction { get; set; }
    }
}
