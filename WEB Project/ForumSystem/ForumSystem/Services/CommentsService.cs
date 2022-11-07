using ForumSystem.Exceptions;
using ForumSystem.Helpers;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.Enums;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services.Contracts;
using Humanizer;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace ForumSystem.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository commentsRepository;
        private readonly IUserAuthorChecker userAuthorChecker;
        private readonly IModelMapper modelMapper;

        public CommentsService(ICommentsRepository commentsRepository, IUserAuthorChecker userAuthorChecker, IModelMapper modelMapper)
        {
            this.commentsRepository = commentsRepository;
            this.userAuthorChecker = userAuthorChecker;
            this.modelMapper = modelMapper;
        }

        public List<CommentDto> GetAll()
        {
            return this.commentsRepository.GetAll()
                                          .Select(c => this.modelMapper.ToDto(c))
                                          .ToList();
        }

        public List<CommentDto> GetByPostId(int postId)
        {
            return this.commentsRepository.GetCommentByPostId(postId)
                                          .Select(c => this.modelMapper.ToDto(c))
                                          .ToList();
        }

        public CommentDto GetById(int postId, int commentId)
        {
            Comment comment = this.commentsRepository.GetCommentById(postId, commentId);
            return this.modelMapper.ToDto(comment);
        }

        public CommentDto GetById(int commentId)
        {
            Comment comment = this.commentsRepository.GetCommentById(commentId);
            return this.modelMapper.ToDto(comment);
        }

        public List<CommentDto> FilterBy(CommentQueryParameters filterParameters)
        {
            return this.commentsRepository.FilterBy(filterParameters)
                                          .Select(c => this.modelMapper.ToDto(c))
                                          .ToList();
        }

        public CommentDto Create(Comment comment)
        {
            return this.modelMapper.ToDto(this.commentsRepository.Create(comment));
        }

        public CommentReactionDto CreateCommentReaction(int postId, int commentId, int userId, CommentReaction commentReaction)
        {
            Comment commentToReact = this.commentsRepository.GetCommentById(postId, commentId);

            if (commentToReact.CommentReactions.FirstOrDefault(x => x.UserId == userId) != null)
            {
                int reactId = commentToReact.CommentReactions.FirstOrDefault(x => x.UserId == userId).Id;
                CommentReaction updatedCommentReaction = this.commentsRepository.UpdateCommentReaction(postId, commentId, reactId, commentReaction);
                if (updatedCommentReaction == null)
                {
                    return null;
                }
                else
                {
                    return this.modelMapper.CommentReactionMap(updatedCommentReaction);
                }

            }

            CommentReaction createdCommentReaction = this.commentsRepository.CreateCommentReaction(commentReaction);
            commentToReact.CommentReactions.Add(createdCommentReaction);
            return this.modelMapper.CommentReactionMap(createdCommentReaction);

        }

        public CommentDto Update(int postId, int commentId, Comment comment, int authUserId)
        {

            Comment commentToUpdate = this.commentsRepository.GetCommentById(postId, commentId);
            return this.modelMapper.ToDto(this.commentsRepository.Update(postId, commentToUpdate.Id, comment));

        }

        public CommentReactionDto UpdateCommentReaction(int postId, int commentId, int commentReactId, CommentReaction commentReaction, int authUserId)
        {
            try
            {
                int commentReactUserId = (int)this.GetCommentReactionUserId(commentReactId);
                bool isUserAuthor = this.userAuthorChecker.IsUserAuthor(authUserId, commentReactUserId);
                Comment commentToReact = this.commentsRepository.GetCommentById(postId, commentId);

                return this.modelMapper.CommentReactionMap(this.commentsRepository.UpdateCommentReaction(postId, commentToReact.Id, commentReactId, commentReaction));
            }
            catch (UnauthorizedOperationException)
            {
                throw;
            }

        }

        public void Delete(int postId, int commentId, User authUser)
        {
            try
            {

                Comment commentToDelete = this.commentsRepository.GetCommentById(postId, commentId);

                if (commentToDelete.UserId != authUser.Id && !authUser.Role.Name.Equals("Admin"))
                {
                    throw new UnauthorizedOperationException("You are not authorized to delete this comment!");
                }
                this.commentsRepository.Delete(postId, commentToDelete.Id);
            }
            catch (UnauthorizedOperationException)
            {
                throw;
            }

        }

        public void DeleteCommentReaction(int postId, int commentId, int commentReactId, int userId)
        {
            try
            {
                int commentReactionUserId = (int)this.GetCommentReactionUserId(commentReactId);
                bool isUserAuthor = this.userAuthorChecker.IsUserAuthor(userId, commentReactionUserId);
                Comment commentToReact = this.commentsRepository.GetCommentById(postId, commentId);
                this.commentsRepository.DeleteCommentReaction(postId, commentToReact.Id, commentReactId);
            }
            catch (UnauthorizedOperationException)
            {
                throw;
            }

        }

        public int? GetCommentReactionUserId(int commentReactId)
        {
            int? commentReactionUserId = this.commentsRepository.GetAllCommentReactions().Where(r => r.Id == commentReactId).FirstOrDefault().UserId;

            return commentReactionUserId ?? throw new EntityNotFoundException("The reaction is not found.");
        }

        public List<CommentDto> GetCommentsByUser(int userId)
        {
            return this.commentsRepository.GetCommentsByUser(userId)
                                          .Select(c => this.modelMapper.ToDto(c))
                                          .ToList();
        }

    }
}
