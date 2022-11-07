using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.Enums;
using ForumSystem.Models.QueryParameters;
using System.Collections.Generic;

namespace ForumSystem.Services.Contracts
{
    public interface ICommentsService
    {
        List<CommentDto> GetAll();
        CommentDto GetById(int postId, int commentId);
        CommentDto GetById(int commentId);
        List<CommentDto> GetByPostId(int postId);
        List<CommentDto> FilterBy(CommentQueryParameters filterParameters);
        CommentDto Create(Comment comment);
        CommentReactionDto CreateCommentReaction(int postId, int commentId, int userId, CommentReaction commentReaction);

        CommentDto Update(int postId, int commentId, Comment comment, int authUserId);
        CommentReactionDto UpdateCommentReaction(int postId, int commentId, int commentReactId, CommentReaction commentReaction, int authUserId);

        void Delete(int postId, int commentId, User authUser);
        void DeleteCommentReaction(int postId, int commentId, int commentReactId, int userId);
        int? GetCommentReactionUserId(int commentReactId);
        List<CommentDto> GetCommentsByUser(int userId);
    }
}
