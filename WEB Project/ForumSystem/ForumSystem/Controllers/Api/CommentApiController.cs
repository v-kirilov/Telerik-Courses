using ForumSystem.Exceptions;
using ForumSystem.Helpers;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ForumSystem.Controllers.Api
{
    [ApiController]
    [Route("api/posts")]
    public class CommentApiController : ControllerBase
    {
        private readonly ICommentsService commentsService;
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelmapper;
        private readonly IUserAuthorChecker userAuthorChecker;
        public CommentApiController(ICommentsService commentsService, IAuthManager authManager, IModelMapper modelmapper, IUserAuthorChecker userAuthorChecker)
        {
            this.commentsService = commentsService;
            this.authManager = authManager;
            this.modelmapper = modelmapper;
            this.userAuthorChecker = userAuthorChecker;
        }


        [HttpGet("comments")]
        public IActionResult GetFilteredComments([FromQuery] CommentQueryParameters filterParameters)
        {
            try
            {
                List<CommentDto> result = this.commentsService.FilterBy(filterParameters);

                return this.StatusCode(StatusCodes.Status200OK, result);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }

        }

        [HttpGet("{postId}/comments")]
        public IActionResult GetCommentsByPostId(int postId)
        {
            try
            {
                IEnumerable<CommentDto> result = this.commentsService.GetByPostId(postId);

                return this.StatusCode(StatusCodes.Status200OK, result);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("{postId}/comments/{commentId}")]
        public IActionResult GetCommentById(int postId, int commentId)
        {
            try
            {
                CommentDto comment = this.commentsService.GetById(postId, commentId);

                return this.StatusCode(StatusCodes.Status200OK, comment);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }


        [HttpPost("{postId}/comments")]
        public IActionResult CreateComment(int postId, [FromHeader] string username, [FromBody] CommentDto commentDto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                //bool isBlocked = !this.authManager.IsUserBlocked(username);

                Comment comment = this.modelmapper.ToModel(commentDto, postId, authUser);
                CommentDto createdComment = this.commentsService.Create(comment);

                return this.StatusCode(StatusCodes.Status201Created, createdComment);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return this.StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }

        }

        [HttpPost("{postId}/comments/{commentId}/reactions")]
        public IActionResult CreateCommentReaction(int postId, int commentId, [FromHeader] string username, [FromBody] CommentReactionDto commentReactionDto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                //bool isBlocked = !this.authManager.IsUserBlocked(username);

                int userId = authUser.Id;
                CommentReaction commentReaction = this.modelmapper.ToModel(commentReactionDto, commentId, authUser);
                CommentReactionDto createdCommentReaction = this.commentsService.CreateCommentReaction(postId, commentId, userId, commentReaction);

                return this.StatusCode(StatusCodes.Status201Created, createdCommentReaction);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return this.StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (DuplicateEntityException e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }

        }

        [HttpPut("{postId}/comments/{commentId}")]
        public IActionResult UpdateComment(int postId, int commentId, [FromHeader] string username, [FromBody] CommentDto commentDto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                //bool isBlocked = !this.authManager.IsUserBlocked(username);                
                
                Comment comment = this.modelmapper.ToModel(commentDto, postId, authUser);
                bool isAuthor = this.userAuthorChecker.IsUserAuthor(authUser.Id, (int)comment.UserId);
                CommentDto updatedComment = this.commentsService.Update(postId, commentId, comment, authUser.Id);

                return this.StatusCode(StatusCodes.Status200OK, updatedComment);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return this.StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }

        }

        [HttpPut("{postId}/comments/{commentId}/reactions/{commentReactId}")]
        public IActionResult UpdateCommentReaction(int postId, int commentId, int commentReactId, [FromHeader] string username, [FromBody] CommentReactionDto commentReactionDto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                //bool isBlocked = !this.authManager.IsUserBlocked(username);

                int userId = authUser.Id;
                CommentReaction commentReaction = this.modelmapper.ToModel(commentReactionDto, commentId, authUser);
                CommentReactionDto updatedCommentReaction = this.commentsService.UpdateCommentReaction(postId, commentId, commentReactId, commentReaction, userId);

                return this.StatusCode(StatusCodes.Status200OK, updatedCommentReaction);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return this.StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpDelete("{postId}/comments/{commentId}")]
        public IActionResult DeleteComment(int postId, int commentId, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                //bool isBlocked = !this.authManager.IsUserBlocked(username);

                this.commentsService.Delete(postId, commentId, authUser);

                return this.StatusCode(StatusCodes.Status200OK);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return this.StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpDelete("{postId}/comments/{commentId}/reactions/{commentReactId}")]
        public IActionResult DeleteCommentReaction(int postId, int commentId, int commentReactId, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                //bool isBlocked = !this.authManager.IsUserBlocked(username);

                int userId = authUser.Id;
                this.commentsService.DeleteCommentReaction(postId, commentId, commentReactId, userId);

                return this.StatusCode(StatusCodes.Status200OK);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return this.StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

    }
}
