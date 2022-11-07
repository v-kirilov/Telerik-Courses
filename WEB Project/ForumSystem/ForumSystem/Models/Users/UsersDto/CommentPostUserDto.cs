using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO.Users
{
    public class CommentPostUserDto
    {
        public CommentPostUserDto(Comment comment)
        {
            this.Id = comment.Id;
            this.CommentContent = comment.CommentContent;
            this.Author = comment.User.Username;
            this.CommentReactions = comment.CommentReactions.Select(r =>  new CommentReactionDto(r)).ToList();
            this.Post = new PostsCommentUserDto(comment.Post);
        }

        public int Id { get; set; }
        public string CommentContent { get; set; }
        public string Author { get; set; }
        public ICollection<CommentReactionDto> CommentReactions { get; set; }
        public PostsCommentUserDto Post { get; set; }
    }
}
