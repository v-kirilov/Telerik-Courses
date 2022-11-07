using ForumSystem.Models.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Models.DTO
{
    public class CommentsUserDto
    {
        public CommentsUserDto(Comment comment)
        {
            this.Id = comment.Id;
            this.Content = comment.CommentContent;
            this.Post = new PostsCommentUserDto(comment.Post);
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public PostsCommentUserDto Post { get; set; }
    }
}
