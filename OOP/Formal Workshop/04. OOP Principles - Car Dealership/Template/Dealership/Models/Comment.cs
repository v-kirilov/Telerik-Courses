
using Dealership.Models.Contracts;
using System.Text;

namespace Dealership.Models
{
    public class Comment : IComment
    {
        public const int CommentMinLength = 3;
        public const int CommentMaxLength = 200;
        public const string InvalidCommentError = "Content must be between 3 and 200 characters long!";

        public Comment(string content, string author)
        {
            Validator.ValidateIntRange(content.Length,CommentMinLength,CommentMaxLength,InvalidCommentError);
            this.Content = content;
            this.Author = author;
        }


        public string Content
        {
            get;
        }
        private string author;

        public string Author
        {
            get { return this.author; }
            private set { this.author = value; }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("    ----------");
            sb.AppendLine($"    {this.Content}");
            sb.AppendLine($"      User: {this.Author}");
            sb.AppendLine("    ----------");


            return sb.ToString();
        }


    }
}
