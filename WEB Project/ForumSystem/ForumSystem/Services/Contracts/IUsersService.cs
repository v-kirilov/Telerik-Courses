using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.DTO.Contracts;
using ForumSystem.Models.DTO.Users;
using ForumSystem.Models.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Services.Contracts
{
    public interface IUsersService
    {
        List<User> GetAll();
        IUserDto GetById(int id);
        User GetUserById(int id);
        User GetByUsername(string username);
        List<IUserDto> FilterBy(UserQueryParameters filterParameters);
        PaginatedList<User> FilterUserBy(UserQueryParameters filterParameters);
        IUserDto Create(CreateUserDto userDto);
        IUserDto Update(int id, UpdateUserDto userDto, User authUser);
        void Delete(int id, User authUser);
        IUserDto GetSpecificUserPosts(int id);
        UserSpecificPostDto GetUserSpecificPost(int userId, int postId);
        UserSpecificPostDto CreateSpecificUserPost(int id, PostDto postDto);
        UserSpecificPostDto UpdateUserSpecificPost(int userId, int postId, PostDto post, User authUser);
        void DeleteUserSpecificPost(int userId, int postId, User authUser);
        IUserDto GetSpecificUserComments(int id);
        CommentPostUserDto GetUserSpecificComment(int userId, int commentId);
        CommentPostUserDto UpdateUserSpecificComment(int userId, int commentId, CommentDto commentDto, User authUser);
        void DeleteUserSpecificComment(int userId, int commentId, User authUser);
        List<PostSpecificCommentDto> GetUserPostAllComments(int userId, int postId);
        PostSpecificCommentDto CreateUserPostAllComment(int userId, int postId, CommentDto commentDto, User authUser);
        PostSpecificCommentDto GetUserPostSpecificComment(int userId, int postId, int commentId);
        PostSpecificCommentDto UpdateUserPostSpecificComment(int userId, int postId, int commentId, User authUser, CommentDto commentDto);
        void DeleteUserPostSpecificComment(int userId, int postId, int commentId, User authUser);
        List<UserPostReactionDto> GetUserSpecificPostReactions(int userId, int postId);
        UserPostReactionDto CreateUserSpecificPostReaction(int userId, int postId, PostReactionDto postReactionDto, User author);
        UserPostReactionDto GetUserPostSpecificReaction(int userId, int postId, int reactionId);
        UserPostReactionDto UpdateUserPostSpecificReaction(int userId, int postId, int reactionId, PostReactionDto postReactionDto, User authUser);
        void DeleteUserPostSpecificReaction(int userId, int postId, int reactionId, User authUser);
        List<UserCommentReactionDto> GetUserSpecificCommentReactions(int userId, int commentId);
        UserCommentReactionDto CreateUserSpecificCommentReaction(int userId, int commentId, CommentReactionDto commentReactionDto, User author);
        UserCommentReactionDto GetUserCommentSpecificReaction(int userId, int commentId, int reactionId);
        UserCommentReactionDto UpdateUserCommentSpecificReaction(int userId, int commentId, int reactionId, CommentReactionDto commentReactionDto, User authUser);
        void DeleteUserCommentSpecificReaction(int userId, int commentId, int reactionId, User authUser);
        bool UsernameExists(string username);
        bool EmailExists(string email);
    }
}
