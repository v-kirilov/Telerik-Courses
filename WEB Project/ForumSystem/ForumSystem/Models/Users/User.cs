using ForumSystem.Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }        
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        public string ProfilePicture { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CommentReaction> CommentReactions { get; set; }
        public ICollection<PostReaction> PostReactios { get; set; }
    }
}
