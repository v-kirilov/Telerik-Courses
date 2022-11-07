using ForumSystem.Exceptions;
using ForumSystem.Helpers;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace ForumSystem.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelMapper;
        private readonly IUsersService usersService;
        private readonly ICommentsService commentsService;
        private readonly IUserAuthorChecker authorChecker;

        public PostsController(IPostsService postsService, IAuthManager authManager, IModelMapper modelMapper, IUsersService usersService, ICommentsService commentsService, IUserAuthorChecker authorChecker)
        {
            this.authManager = authManager;
            this.postsService = postsService;
            this.modelMapper = modelMapper;
            this.usersService = usersService;
            this.commentsService = commentsService;
            this.authorChecker = authorChecker;
        }

        [HttpGet]
        public IActionResult Index(string filter, string search, PostQueryParameters query, string sort)
        {
            try
            {
                if (filter == "Title")
                {
                    query.Title = search;
                }
                else if (filter == "Content")
                {
                    query.Content = search;
                }
                else if (filter == "Username")
                {
                    var user = usersService.GetByUsername(search);
                    query.User = user.Username;
                }
                if (sort == "Popular")
                {
                    query.HasMostPopular = true;
                }
                else if (sort == "Recent")
                {
                    query.HasMostRecent = true;
                }

                PaginatedList<PostDto> posts = this.postsService.FilterBy(query);

                return View(posts);
            }
            catch (EntityNotFoundException x)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = x.Message;
                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            var postDto = new PostDto();
            return this.View(postDto);
        }

        [HttpPost]
        public IActionResult Create(PostDto postDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(postDto);
            }
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var user = this.authManager.CurrentUser;
            //postDto.UserId = user.Id;
            postDto.User = user.Username;
            var post = modelMapper.MapPostCreate(postDto, user.Id);
            var createdPost = this.postsService.Create(post);
            return this.RedirectToAction("Details", "Posts", new { id = createdPost.Id });
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            try
            {
                var post = this.postsService.GetById(id);
                PostMVCDto postForView = this.modelMapper.ToMVCDto(post);
                return this.View(postForView);
            }
            catch (EntityNotFoundException x)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = x.Message;
                return this.View("Error");
            }
        }

        [HttpPost]
        public IActionResult Details(PostMVCDto postForView)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(postForView);
            }
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var user = this.authManager.CurrentUser;
            //postDto.UserId = user.Id;
            //postForView.User = user.Username;
            var newComment = postForView.Comment;
            var comment = modelMapper.ToModel(newComment, postForView.Id, user);
            this.commentsService.Create(comment);
            return this.RedirectToAction("Details", "Posts", postForView.Id);
        }

        [HttpGet]
        public IActionResult PostLikes([FromRoute] int id)
        {

            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var post = this.postsService.GetById(id);
            //PostMVCDto postForView = this.modelMapper.ToMVCDto(post);
            var user = this.authManager.CurrentUser;
            PostReactionDto newReact = new PostReactionDto()
            {
                Reaction = "Like"
            };
            PostReaction postReact = modelMapper.MapPostReactionCreate(post.Id, user, newReact);
            try
            {
                this.postsService.CreatePostReaction(post.Id, postReact);

            }
            catch (DuplicateEntityException x)
            {
                this.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = x.Message;
                return this.View("Error");
            }

            return this.RedirectToAction("Details", "Posts", new { id = post.Id });
        }

        [HttpGet]
        public IActionResult PostDislikes([FromRoute] int id)
        {

            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var post = this.postsService.GetById(id);
            //PostMVCDto postForView = this.modelMapper.ToMVCDto(post);
            var user = this.authManager.CurrentUser;
            PostReactionDto newReact = new PostReactionDto()
            {
                Reaction = "Dislike"
            };
            PostReaction postReact = modelMapper.MapPostReactionCreate(post.Id, user, newReact);
            this.postsService.CreatePostReaction(post.Id, postReact);

            return this.RedirectToAction("Details", "Posts", new { id = post.Id });
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            try
            {
                var user = this.authManager.CurrentUser;
                bool isBlocked = authManager.IsUserBlocked(user.Username);
                bool isAuthorOrAdmin = postsService.IsAuthorOrAdmin(user, id);
                var post = this.postsService.GetById(id);
                return this.View(post);

            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;

                return this.View("Error");
            }
            catch (BlockedUserException e)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            try
            {
                this.postsService.DeletePost(id);
                return this.RedirectToAction("Index", "Posts");
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;

                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult Update([FromRoute] int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            try
            {
                var user = this.authManager.CurrentUser;
                bool isBlocked = authManager.IsUserBlocked(user.Username);
                bool isAuthorOrAdmin = postsService.IsAuthorOrAdmin(user, id);

                var post = this.postsService.GetById(id);
                return this.View(post);

            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            catch (BlockedUserException e)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpPost]
        public IActionResult Update([FromRoute] int id, PostDto postDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(postDto);
            }
            try
            {
                var user = this.authManager.CurrentUser;

                var updatedPost = this.postsService.Update(id, postDto);
                return this.RedirectToAction("Index", "Posts");

            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;

                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult UpdateComment([FromRoute] int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            try
            {
                var user = this.authManager.CurrentUser;
                
                var comment = this.commentsService.GetById(id);
                var commentAuthor = this.usersService.GetByUsername(comment.Author);
                bool isAuthor = this.authorChecker.IsUserAuthor(user.Id, commentAuthor.Id);
                
                return this.View(comment);

            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpPost]
        public IActionResult UpdateComment([FromRoute] int id, CommentDto commentDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(commentDto);
            }
            try
            {
                var user = this.authManager.CurrentUser;
                var comment = this.modelMapper.ToModel(commentDto);
                var commentToUpdate = this.commentsService.GetById(id);
                var updatedComment = this.commentsService.Update(commentToUpdate.PostId, id, comment, user.Id);
                return this.RedirectToAction("Details", "Posts", new { id = updatedComment.PostId});

            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;

                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult DeleteComment([FromRoute] int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            try
            {
                var user = this.authManager.CurrentUser;

                var comment = this.commentsService.GetById(id);
                
                return this.View(comment);

            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpPost, ActionName("DeleteComment")]
        public IActionResult DeleteCommentConfirmed([FromRoute] int id)
        {
            try
            {
                var user = this.authManager.CurrentUser;
                var comment = this.commentsService.GetById(id);
                var commentAuthor = this.usersService.GetByUsername(comment.Author);
                bool isAuthor = this.authorChecker.IsUserAuthor(user.Id, commentAuthor.Id);
                
                this.commentsService.Delete(comment.PostId, id, commentAuthor);
                return this.RedirectToAction("Details", "Posts", new { id = comment.PostId });

            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;

                return this.View("Error");
            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult CommentLikes([FromRoute] int id)
        {

            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var comment = this.commentsService.GetById(id);
            var user = this.authManager.CurrentUser;

            CommentReactionDto newReact = new CommentReactionDto()
            {
                Reaction = "Like"
            };
            CommentReaction commentReact = modelMapper.ToModel(newReact, id, user);
            try
            {
                this.commentsService.CreateCommentReaction(comment.PostId, id, user.Id, commentReact);

                return this.RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult CommentDislikes([FromRoute] int id)
        {

            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var comment = this.commentsService.GetById(id);
            var user = this.authManager.CurrentUser;
            CommentReactionDto newReact = new CommentReactionDto()
            {
                Reaction = "Dislike"
            };
            CommentReaction commentReact = modelMapper.ToModel(newReact, id, user);
            try
            {
                this.commentsService.CreateCommentReaction(comment.PostId, id, user.Id, commentReact);

                return this.RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            
        }

        [HttpGet]
        public IActionResult IndexComments([FromRoute] int id)
        {
            
            try
            {
                var comments = this.commentsService.GetCommentsByUser(id);

                return View(comments);
            }
            catch (EntityNotFoundException x)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = x.Message;
                return this.View("Error");
            }
        }

    }
}
