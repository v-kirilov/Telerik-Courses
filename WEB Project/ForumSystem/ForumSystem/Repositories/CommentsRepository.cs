using ForumSystem.Data;
using ForumSystem.Exceptions;
using ForumSystem.Helpers;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ForumSystem.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationContext context;

        public CommentsRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public List<Comment> GetAll()
        {
            return this.GetComments().ToList();
        }

        public IEnumerable<Comment> GetCommentByPostId(int postId)
        {
            IEnumerable<Comment> commentList = this.GetComments().Where(b => b.PostId == postId);
            if (commentList.Any())
            {
                return commentList;
            }
            else
            {
                throw new EntityNotFoundException($"The post with id: {postId} has not any comment.");
            }
        }

        public Comment GetCommentById(int postId, int commentId)
        {

            Comment comment = this.GetCommentByPostId(postId).Where(b => b.Id == commentId).FirstOrDefault();

            return comment ?? throw new EntityNotFoundException("The comment is not found.");
        }

        public Comment GetCommentById(int commentId)
        {

            Comment comment = this.GetComments().Where(c => c.Id == commentId).FirstOrDefault();

            return comment ?? throw new EntityNotFoundException("The comment is not found.");
        }

        public List<Comment> FilterBy(CommentQueryParameters filterParameters)
        {
            List<Comment> result = this.GetAll();
            
            if (!string.IsNullOrEmpty(filterParameters.Content))
            {
                result = result.FindAll(x => x.CommentContent.Contains(filterParameters.Content));
            }
            
            if (result.Any())
            {
                return result;
            }
            else
            {
                throw new EntityNotFoundException("The content is not found.");
            }
            

        }


        public Comment Create(Comment comment)
        {
            this.context.Comments.Add(comment);
            this.context.SaveChanges();

            return comment;
        }

        public CommentReaction CreateCommentReaction(CommentReaction commentReaction)
        {

            this.context.CommentReactions.Add(commentReaction);
            this.context.SaveChanges();
            return commentReaction;
        }
        public Comment Update(int postId, int commentId, Comment comment)
        {
            Comment commentToUpdate = this.GetCommentById(postId, commentId);
            commentToUpdate.CommentContent = comment.CommentContent;
            this.context.Update(commentToUpdate);
            this.context.SaveChanges();

            return commentToUpdate;
        }

        public CommentReaction UpdateCommentReaction(int postId, int commentId, int reactionId, CommentReaction commentReaction)
        {
            Comment comment = this.GetCommentById(postId, commentId);
            CommentReaction reactionToUpdate = comment.CommentReactions.FirstOrDefault(x => x.Id == reactionId);
            if (reactionToUpdate == null)
            {
                throw new EntityNotFoundException("The reaction is not found.");
            }
            if (reactionToUpdate.Reaction == commentReaction.Reaction)
            {
                this.DeleteCommentReaction(postId, commentId, reactionId);
                return null;
            }
            reactionToUpdate.Reaction = commentReaction.Reaction;
            this.context.Update(reactionToUpdate);
            this.context.SaveChanges();

            return reactionToUpdate;
        }

        public void Delete(int postId, int commentId)
        {
            Comment commentToDelete = this.GetCommentById(postId, commentId);
            this.context.Comments.Remove(commentToDelete);
            this.context.SaveChanges();
        }

        public void DeleteCommentReaction(int postId, int commentId, int commentReactId)
        {
            Comment comment = this.GetCommentById(postId, commentId);
            CommentReaction reactionToDelete = comment.CommentReactions.FirstOrDefault(x => x.Id == commentReactId);
            if (reactionToDelete == null)
            {
                throw new EntityNotFoundException("The reaction is not found.");
            }
            this.context.CommentReactions.Remove(reactionToDelete);
            this.context.SaveChanges();
        }

        public List<CommentReaction> GetAllCommentReactions()
        {
            return this.GetCommentReactions().ToList();
        }

        private IQueryable<Comment> GetComments()
        {
            return this.context.Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                .Include(c => c.CommentReactions)
                  .ThenInclude(r => r.User);
               
        }

        private IQueryable<CommentReaction> GetCommentReactions()
        {
            return this.context.CommentReactions
                .Include(c => c.User)
                .Include(c => c.Comment);
        }

        public List<Comment> GetCommentsByUser(int userId)
        {
            List<Comment> commentList = this.GetComments().Where(b => b.UserId == userId).ToList();
            if (commentList.Any())
            {
                return commentList;
            }
            else
            {
                throw new EntityNotFoundException($"The user with id: {userId} has not any comment.");
            }
        }
    }
}

