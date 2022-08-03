using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Exceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Models
{
    public class Comment : IComment
    {
        public const int CommentMinLength = 3;
        public const int CommentMaxLength = 200;
        public const string InvalidCommentError = "Content must be between {0} and {1} characters long!";
        public const string NullOrEmptyCommentError = "Please provide non-empty value for comment";

        public const string NullOrEmptyAuthorError = "Please provide non-empty value for author";

        public Comment(string content, string author)
        {
            var errorMsg = string.Format(Comment.InvalidCommentError, Comment.CommentMinLength, Comment.CommentMaxLength);
            Validator.ValidateIntRange(content.Length, Comment.CommentMinLength, Comment.CommentMaxLength, errorMsg);
            Validator.ValidateStringIsNullOrEmpty(content, Comment.NullOrEmptyCommentError);
            this.Content = content;

            Validator.ValidateStringIsNullOrEmpty(author, Comment.NullOrEmptyAuthorError);
            this.Author = author;
        }
        public string Content { get; }

        public string Author { get; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"   Author: {this.Author}");
            sb.AppendLine($"   Content: {this.Content}");

            return sb.ToString().Trim();
        }
    }
}
