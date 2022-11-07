using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.QueryParameters;
using System.Collections.Generic;

namespace ForumSystem.Repositories.Contracts
{
    public interface ICommentsRepository
    {
        List<Comment> GetAll();        
        IEnumerable<Comment> GetCommentByPostId(int postId);
        Comment GetCommentById(int commentId);
        Comment GetCommentById(int postId, int commentId);
        List<Comment> FilterBy(CommentQueryParameters filterParameters);
        Comment Create(Comment comment);
        CommentReaction CreateCommentReaction(CommentReaction commentReaction);

        Comment Update(int postId, int commentId, Comment comment);
        CommentReaction UpdateCommentReaction(int postId, int commentId, int reactionId, CommentReaction commentReaction);

        void Delete(int postId, int commentId);
        void DeleteCommentReaction(int postId, int commentId, int commentReactId);
        List<CommentReaction> GetAllCommentReactions();
        List<Comment> GetCommentsByUser(int userId);

    }
}
