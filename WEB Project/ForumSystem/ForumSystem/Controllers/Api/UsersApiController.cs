using ForumSystem.Exceptions;
using ForumSystem.Helpers;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.DTO.Contracts;
using ForumSystem.Models.DTO.Users;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Controllers.Api
{
    [Route("api/users")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelMapper;

        public UsersApiController(IUsersService usersService, IAuthManager authManager, IModelMapper modelMapper)
        {
            this.usersService = usersService;
            this.authManager = authManager;
            this.modelMapper = modelMapper;
        }
        
        #region Users
        [HttpGet("")]
        public IActionResult GetUsers([FromQuery] UserQueryParameters filterParameters)
        {
            try
            {
                List<IUserDto> result = this.usersService.FilterBy(filterParameters);

                return this.StatusCode(StatusCodes.Status200OK, result);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                IUserDto user = this.usersService.GetById(id);
                
                return this.StatusCode(StatusCodes.Status200OK, user);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreateUser([FromBody] CreateUserDto dto)
        {
            try
            {
                
                IUserDto createdUser = this.usersService.Create(dto);
                    
                return this.StatusCode(StatusCodes.Status201Created, createdUser);
            }
            catch (DuplicateEntityException e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromHeader] string username, [FromBody] UpdateUserDto dto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                
                IUserDto updatedUser = this.usersService.Update(id, dto, authUser);

                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                this.usersService.Delete(id, authUser);

                return this.StatusCode(StatusCodes.Status200OK);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }
        #endregion

        #region Posts
        // POSTS Endpoints

        [HttpGet("{id}/posts")]
        public IActionResult GetSpecificUserPosts(int id)
        {
            try
            {
                IUserDto user = this.usersService.GetSpecificUserPosts(id);

                return this.StatusCode(StatusCodes.Status200OK, user);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("{id}/posts")]
        public IActionResult CreateSpecificUserPost(int id, [FromBody] PostDto postDto)
        {
            try
            {
                
                UserSpecificPostDto createdPost = this.usersService.CreateSpecificUserPost(id, postDto);
                return this.StatusCode(StatusCodes.Status200OK, createdPost);
            }
            catch (DuplicateEntityException e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
            catch (EntityNotFoundException)
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable, "UserId required");
            }
            catch (BlockedUserException e)
            {
                return this.StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
        }

        [HttpGet("{userId}/posts/{postId}")]
        public IActionResult GetUserSpecificPost(int userId, int postId)
        {
            try
            {
                UserSpecificPostDto post = this.usersService.GetUserSpecificPost(userId, postId);

                return this.StatusCode(StatusCodes.Status200OK, post);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPut("{userId}/posts/{postId}")]
        public IActionResult UpdateUserSpecificPost(int userId, int postId, [FromBody] PostDto postDto, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                UserSpecificPostDto updatedPost = this.usersService.UpdateUserSpecificPost(userId, postId, postDto, authUser);

                return this.StatusCode(StatusCodes.Status200OK, updatedPost);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (DuplicateEntityException e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }

        [HttpDelete("{userId}/posts/{postId}")]
        public IActionResult DeleteUserSpecificPost(int userId, int postId, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                this.usersService.DeleteUserSpecificPost(userId, postId, authUser);

                return this.StatusCode(StatusCodes.Status200OK);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }
        #endregion

        #region Post reactions
        // POST REACTIONS Endpoints
        [HttpGet("{userId}/posts/{postId}/reactions")]
        public IActionResult GetUserSpecificPostReactions(int userId, int postId)
        {
            try
            {
                List<UserPostReactionDto> reactions = this.usersService.GetUserSpecificPostReactions(userId, postId);
                return this.StatusCode(StatusCodes.Status200OK, reactions);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (InvalidDataException)
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable, "Invalid reaction or post Id");
            }
        }

        [HttpPost("{userId}/posts/{postId}/reactions")]
        public IActionResult CreateUserSpecificPostReaction(int userId, int postId, [FromBody]PostReactionDto postReactionDto, [FromHeader] string username)
        {
            try
            {
                var user = this.authManager.TryGetUser(username);
                var cratedPostReactions = this.usersService.CreateUserSpecificPostReaction(userId, postId, postReactionDto, user);
                return this.StatusCode(StatusCodes.Status200OK, cratedPostReactions);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (InvalidDataException)
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable, "Invalid reaction or post Id");
            }
            catch (DuplicateEntityException)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, "This user already reacted to this post!");
            }
        }

        [HttpGet("{userId}/posts/{postId}/reactions/{reactionId}")]
        public IActionResult GetUserPostSpecificReaction(int userId, int postId, int reactionId)
        {
            try
            {
                var postReaction = this.usersService.GetUserPostSpecificReaction(userId, postId, reactionId);
                return this.StatusCode(StatusCodes.Status200OK, postReaction);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (InvalidDataException)
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable, "Invalid reaction or post Id");
            }
        }

        [HttpPut("{userId}/posts/{postId}/reactions/{reactionId}")]
        public IActionResult UpdateUserPostSpecificReaction(int userId, int postId, int reactionId, [FromBody] PostReactionDto postReactionDto, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                var postReaction = this.usersService.UpdateUserPostSpecificReaction(userId, postId, reactionId, postReactionDto, authUser);

                return this.StatusCode(StatusCodes.Status200OK, postReaction);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (InvalidDataException)
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable, "Invalid reaction or post Id");
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }

        [HttpDelete("{userId}/posts/{postId}/reactions/{reactionId}")]
        public IActionResult DeleteUserPostSpecificReaction(int userId, int postId, int reactionId, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                this.usersService.DeleteUserPostSpecificReaction(userId, postId, reactionId, authUser);

                return this.StatusCode(StatusCodes.Status200OK);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (InvalidDataException)
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable, "Invalid reaction or post Id");
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }
        #endregion

        #region Comments
        // COMMENTS Endpoints

        [HttpGet("{id}/comments")]
        public IActionResult GetSpecificUserComments(int id)
        {
            try
            {
                IUserDto user = this.usersService.GetSpecificUserComments(id);

                return this.StatusCode(StatusCodes.Status200OK, user);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("{userId}/comments/{commentId}")]
        public IActionResult GetUserSpecificComment(int userId, int commentId)
        {
            try
            {
                CommentPostUserDto comment = this.usersService.GetUserSpecificComment(userId, commentId);

                return this.StatusCode(StatusCodes.Status200OK, comment);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPut("{userId}/comments/{commentId}")]
        public IActionResult UpdateUserSpecificComment(int userId, int commentId, [FromBody] CommentDto commentDto, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                CommentPostUserDto updatedComment = this.usersService.UpdateUserSpecificComment(userId, commentId, commentDto, authUser);

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

        [HttpDelete("{userId}/comments/{commentId}")]
        public IActionResult DeleteUserSpecificComment(int userId, int commentId, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                this.usersService.DeleteUserSpecificComment(userId, commentId, authUser);

                return this.StatusCode(StatusCodes.Status200OK);
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
        #endregion

        #region Comment reactions
        // Comment reactions endpoints
        [HttpGet("{userId}/comments/{commentId}/reactions")]
        public IActionResult GetUserSpecificCommentReactions(int userId, int commentId)
        {
            try
            {
                List<UserCommentReactionDto> reactions = this.usersService.GetUserSpecificCommentReactions(userId, commentId);
                return this.StatusCode(StatusCodes.Status200OK, reactions);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (InvalidDataException)
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable, "Invalid reaction or comment Id");
            }
        }

        [HttpPost("{userId}/comments/{commentId}/reactions")]
        public IActionResult CreateUserSpecificCommentReactions(int userId, int commentId, [FromBody] CommentReactionDto commentReactionDto,
            [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                UserCommentReactionDto reactions = this.usersService.CreateUserSpecificCommentReaction(userId, commentId, commentReactionDto, authUser);
                return this.StatusCode(StatusCodes.Status200OK, reactions);
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
            catch (InvalidDataException e)
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable, e.Message);
            }
        }

        [HttpGet("{userId}/comments/{commentId}/reactions/{reactionId}")]
        public IActionResult GetUserCommentSpecificReaction(int userId, int commentId, int reactionId)
        {
            try
            {
                UserCommentReactionDto reactions = this.usersService.GetUserCommentSpecificReaction(userId, commentId, reactionId);
                return this.StatusCode(StatusCodes.Status200OK, reactions);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPut("{userId}/comments/{commentId}/reactions/{reactionId}")]
        public IActionResult UpdateUserCommentSpecificReaction(int userId, int commentId, int reactionId, [FromBody] CommentReactionDto commentReactionDto,
            [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                UserCommentReactionDto reactions = this.usersService.UpdateUserCommentSpecificReaction(userId, commentId, reactionId,
                    commentReactionDto, authUser);

                return this.StatusCode(StatusCodes.Status200OK, reactions);
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
            catch (InvalidDataException e)
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable, e.Message);
            }
        }

        [HttpDelete("{userId}/comments/{commentId}/reactions/{reactionId}")]
        public IActionResult DeleteUserCommentSpecificReaction(int userId, int commentId, int reactionId, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                this.usersService.DeleteUserCommentSpecificReaction(userId, commentId, reactionId, authUser);

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
            catch (InvalidDataException e)
            {
                return this.StatusCode(StatusCodes.Status406NotAcceptable, e.Message);
            }
        }

        #endregion

        #region Post and Comments
        // Post and Comments Endpoints
        [HttpGet("{userId}/posts/{postId}/comments")]
        public IActionResult GetUserSpecificPostComments(int userId, int postId)
        {
            try
            {
                List<PostSpecificCommentDto> comments = this.usersService.GetUserPostAllComments(userId, postId);

                return this.StatusCode(StatusCodes.Status200OK, comments);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("{userId}/posts/{postId}/comments")]
        public IActionResult CreateUserSpecificPostComment(int userId, int postId, [FromBody] CommentDto commentDto, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                var comment = this.usersService.CreateUserPostAllComment(userId, postId, commentDto, authUser);

                return this.StatusCode(StatusCodes.Status200OK, comment);
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

        [HttpGet("{userId}/posts/{postId}/comments/{commentId}")]
        public IActionResult GetUserPostSpecificComment(int userId, int postId, int commentId)
        {
            try
            {
                var comment = this.usersService.GetUserPostSpecificComment(userId, postId, commentId);

                return this.StatusCode(StatusCodes.Status200OK, comment);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPut("{userId}/posts/{postId}/comments/{commentId}")]
        public IActionResult UpdateUserPostSpecificComment(int userId, int postId, int commentId, [FromHeader] string username, [FromBody] CommentDto commentDto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                var comment = this.usersService.UpdateUserPostSpecificComment(userId, postId, commentId, authUser, commentDto);

                return this.StatusCode(StatusCodes.Status200OK, comment);
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

        [HttpDelete("{userId}/posts/{postId}/comments/{commentId}")]
        public IActionResult DeleteUserPostSpecificComment(int userId, int postId, int commentId, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                this.usersService.DeleteUserPostSpecificComment(userId, postId, commentId, authUser);

                return this.StatusCode(StatusCodes.Status200OK);
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
        #endregion         
    }
}
