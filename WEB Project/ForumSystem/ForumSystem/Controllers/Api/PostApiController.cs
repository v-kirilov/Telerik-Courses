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
using static Humanizer.In;

namespace ForumSystem.Controllers.Api

{
    [ApiController]
    [Route("api/posts")]
    public class PostApiController : ControllerBase
    {
        private readonly IPostsService postsService;
        private readonly IModelMapper modelMapper;
        private readonly IAuthManager authManager;
        public PostApiController(IPostsService postsService, IModelMapper modelMapper, IAuthManager authManager)
        {
            this.postsService = postsService;
            this.modelMapper = modelMapper;
            this.authManager = authManager;
        }

        [HttpGet("")]
        public IActionResult GetPosts([FromQuery] PostQueryParameters filterPar)
        {
            
            try
            {
                List<PostDto> result = postsService
                .FilterBy(filterPar);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            try
            {
                PostDto post = postsService.GetById(id);
                return StatusCode(StatusCodes.Status200OK, post);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, "There is no posts with that Id!");
            }
        }

        [HttpPost("")]
        public IActionResult CreatePost([FromBody] PostDto postDto, [FromHeader] string username)
        {
            try
            {
                User author = authManager.TryGetUser(username);
                bool isBlocked = authManager.IsUserBlocked(username);
                Post post = modelMapper.MapPostCreate(postDto, author.Id);
                PostDto createdPost = postsService.Create(post);

                return StatusCode(StatusCodes.Status200OK, createdPost);
            }
            catch (DuplicateEntityException e)
            {
                return StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, "UserId required");
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }

        [HttpPost("{id}")]
        public IActionResult CreatePostReaction(int id, [FromBody] PostReactionDto postReactDto, [FromHeader] string username)
        {
            try
            {
                User author = authManager.TryGetUser(username);
                bool isBlocked = authManager.IsUserBlocked(username);

                PostReaction postReact = modelMapper.MapPostReactionCreate(id, author, postReactDto);
                PostReactionDto createdReaction = postsService.CreatePostReaction(id, postReact);
                return StatusCode(StatusCodes.Status200OK, "Post reaction created!");
            }
            catch (InvalidDataException)
            {

                return StatusCode(StatusCodes.Status406NotAcceptable, "Invalid reaction or post Id");
            }
            catch (DuplicateEntityException)
            {
                return StatusCode(StatusCodes.Status409Conflict, "This user already reacted to this post!");
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] PostDto postDto, [FromHeader] string username)
        {
            try
            {
                User author = authManager.TryGetUser(username);
                bool isBlocked = authManager.IsUserBlocked(username);

                bool isAuthor = postsService.IsAuthor(author, id);
                Post post = modelMapper.MapPostCreate(postDto, author.Id);
                PostDto updatePost = postsService.Update(id, postDto);

                return StatusCode(StatusCodes.Status200OK, updatePost);
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (DuplicateEntityException e)
            {
                return StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }

        [HttpPut("{postId}/reactions/")]
        public IActionResult UpdatePostReaction(int postId, [FromBody] PostReactionDto prDto, [FromHeader] string username)
        {
            try
            {
                User author = authManager.TryGetUser(username);
                bool isBlocked = authManager.IsUserBlocked(username);

                PostReaction pr = modelMapper
                    .MapPostReactionCreate(postId, author, prDto);

                PostReactionDto reactionUpdate = postsService
                    .UpdatePostReaction(postId, username, pr);

                return StatusCode(StatusCodes.Status200OK, reactionUpdate);

            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (InvalidDataException e)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost([FromHeader] string username, int id)
        {
            User author = authManager.TryGetUser(username);
            bool isBlocked = authManager.IsUserBlocked(username);
            bool isAuthorOrAdmin = postsService.IsAuthorOrAdmin(author, id);

            try
            {
                postsService.DeletePost(id);
                return StatusCode(StatusCodes.Status200OK, "Post deleted!");
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }

        [HttpDelete("{postId}/reactions/")]
        public IActionResult DeletePostReaction(int postId, [FromHeader] string username)
        {
            try
            {
                User author = authManager.TryGetUser(username);
                bool isBlocked = authManager.IsUserBlocked(username);

                postsService.DeletePostReaction(postId, author);
                return StatusCode(StatusCodes.Status200OK, "Post reaction deleted!");
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (BlockedUserException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }
    }
}
