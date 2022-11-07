using ForumSystem.Exceptions;
using ForumSystem.Helpers;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.DTO.Contracts;
using ForumSystem.Models.DTO.Users;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Services
{
    public class UsersService : IUsersService
    {
        private const string DeletePostReactionErrorMessage = "Only the user himself can delete this post reaction.";
        private const string DeleteCommentErrorMessage = "Only the user himself or the admin can delete this comment.";
        private const string DeletePostErrorMessage = "Only the user himself or the admin can delete this post.";
        private const string ModifyCommentReactionErrorMessage = "Only the user himself can update this comment reaction.";
        private const string ModifyPostReactionErrorMessage = "Only the user himself can update this post reaction.";
        private const string ModifyPostErrorMessage = "Only the user himself can update this post.";
        private const string ModifyCommentErrorMessage = "Only the user himself can update this comment.";
        private const string ModifyUserErrorMessage = "Only the user himself or the admin can update this user's info.";
        private const string ModifyPasswordErrorMessage = "Only the user himself can update this user's password.";
        private const string ModifyUsernameErrorMessage = "The username can't be changed.";
        private const string ModifyRoleErrorMessage = "Only the admin can update this user's role.";
        private const string ModifyBlockStatusErrorMessage = "Only the admin can block or unblock.";
        private const string DeleteUserErrorMessage = "Only the user himself or the admin can delete this user.";
        private const string DuplicateUsernameErrorMessage = "This username already exists. Try with different username.";
        private const string DuplicateEmailErrorMessage = "This email already exists. Try with different email.";
        private const string NoPostReactionsErrorMessage = "No reactions for this post";
        private const string NoCommentReactionsErrorMessage = "No reactions for this comment";
        private const string NoUsersFilterErrorMessage = "There are no users to show";

        private readonly IUsersRepository usersRepository;
        private readonly IPostsService postsService;
        private readonly IModelMapper modelMapper;
        private readonly IPostsRepository postsRepository;
        private readonly ICommentsService commentsService;
        private readonly ICommentsRepository commentsRepository;

        public UsersService(IUsersRepository usersRepository, IPostsService postsService, IModelMapper modelMapper, IPostsRepository postsRepository,
                            ICommentsService commentsService, ICommentsRepository commentsRepository)
        {
            this.usersRepository = usersRepository;
            this.postsService = postsService;
            this.modelMapper = modelMapper;
            this.postsRepository = postsRepository;
            this.commentsService = commentsService;
            this.commentsRepository = commentsRepository;
        }

        #region Users
        public List<User> GetAll()
        {
            return this.usersRepository.GetAll();
        }

        public IUserDto GetById(int id)
        {

             var user = GetUserById(id);
             IUserDto userToReturn = null;
            
             if (user.Role.Name == "Admin")
             {
                 userToReturn = new AdminDto(user);
             }
             else
             {
                 userToReturn = new UserDto(user);
             }
            
             return userToReturn;  
        }

        public User GetUserById(int id)
        {
            var user = this.usersRepository.GetById(id);

            return user;
        }

        public User GetByUsername(string username)
        {
            return this.usersRepository.GetByUsername(username);
        }

        public List<IUserDto> FilterBy(UserQueryParameters filterParameters)
        {
            var users = FilterUserBy(filterParameters);
            List<IUserDto> result = new List<IUserDto>();

            foreach (var user in users)
            {
                if (user.Role.Name == "Admin")
                {
                    result.Add(new AdminDto(user));
                }
                else
                {
                    result.Add(new UserDto(user));
                }
            }

            if (result.Count <= 0)
            {
                throw new EntityNotFoundException(NoUsersFilterErrorMessage);
            }

            return result;
        }

        public PaginatedList<User> FilterUserBy(UserQueryParameters filterParameters)
        {
            var users = this.usersRepository.FilterBy(filterParameters);

            return users;
        }

        public IUserDto Create(CreateUserDto userDto)
        {
            User user = this.modelMapper.MapUserCreate(userDto);

            bool duplicateExists = true;

            try
            {
                this.usersRepository.GetByEmail(user.Email);
            }
            catch (EntityNotFoundException)
            {       
                duplicateExists = false;
            }

            if (duplicateExists)
            {
                throw new DuplicateEntityException(DuplicateEmailErrorMessage);
            }

            duplicateExists = true;

            try
            {
                this.usersRepository.GetByUsername(user.Username);
            }
            catch (EntityNotFoundException)
            {
                duplicateExists = false;
            }

            if (duplicateExists)
            {
                throw new DuplicateEntityException(DuplicateUsernameErrorMessage);
            }

            User createdUser = this.usersRepository.Create(user);

            return new UserDto(createdUser);
        }

        public IUserDto Update(int id, UpdateUserDto userDto, User authUser)
        {
            User user = this.modelMapper.MapUserUpdate(id, userDto, authUser);

            User userToUpdate = this.usersRepository.GetById(id);

            if (!userToUpdate.Username.Equals(authUser.Username) && !authUser.Role.Name.Equals("Admin"))
            {
                throw new UnauthorizedOperationException(ModifyUserErrorMessage);
            }

            if (!string.IsNullOrEmpty(user.Username) && user.Username != userToUpdate.Username)
            {
                throw new UnauthorizedOperationException(ModifyUsernameErrorMessage);
            }

            if (!string.IsNullOrEmpty(user.FirstName))
            {
                userToUpdate = this.usersRepository.UpdateFirstName(id, user.FirstName);
            }

            if (!string.IsNullOrEmpty(user.LastName))
            {
                userToUpdate = this.usersRepository.UpdateLastName(id, user.LastName);
            }

            if (!string.IsNullOrEmpty(user.ProfilePicture))
            {
                userToUpdate = this.usersRepository.UpdateProfilePicture(id, user.ProfilePicture);
            }

            if (!string.IsNullOrEmpty(user.Email) && user.Email != userToUpdate.Email)
            {
                bool duplicateEmail = true;

                try
                {
                    this.usersRepository.GetByEmail(user.Email);
                }
                catch (EntityNotFoundException)
                {
                    duplicateEmail = false;
                }

                if (duplicateEmail == true)
                {
                    throw new DuplicateEntityException(DuplicateEmailErrorMessage);
                }

                userToUpdate = this.usersRepository.UpdateEmail(id, user.Email);
            }

            if (!string.IsNullOrEmpty(user.Password))
            {
                if (userToUpdate.Username.Equals(authUser.Username))
                {
                    userToUpdate = this.usersRepository.UpdatePassword(id, user.Password);
                }
                else
                {
                    throw new UnauthorizedOperationException(ModifyPasswordErrorMessage);
                }                
            }

            if (user.PhoneNumber != null)
            {
                userToUpdate = this.usersRepository.UpdatePhoneNumber(id, user.PhoneNumber);
            }
            else
            {
                userToUpdate = this.usersRepository.UpdatePhoneNumber(id, null);
            }

            if (userToUpdate.Role.Name != user.Role.Name)
            {
                if (authUser.Role.Name.Equals("Admin"))
                {
                    userToUpdate = this.usersRepository.UpdateRole(id, user.Role.Name);
                }
                else
                {
                    throw new UnauthorizedOperationException(ModifyRoleErrorMessage);
                }
            }

            if(userToUpdate.IsBlocked != user.IsBlocked)
            {
                if (authUser.Role.Name.Equals("Admin"))
                {
                    userToUpdate = this.usersRepository.UpdateIsBlocked(id, user.IsBlocked);
                }
                else
                {
                    throw new UnauthorizedOperationException(ModifyBlockStatusErrorMessage);
                }
            }

            return new UserDto(userToUpdate);
        }

        public void Delete(int id, User authUser)
        {

            User userToDelete = this.usersRepository.GetById(id);
            if (!userToDelete.Username.Equals(authUser.Username) && !authUser.Role.Name.Equals("Admin"))
            {
                throw new UnauthorizedOperationException(DeleteUserErrorMessage);
            }

            this.usersRepository.Delete(id);
        }
        #endregion

        #region Posts

        // POSTS
        public IUserDto GetSpecificUserPosts(int id)
        {
            var user = this.usersRepository.GetById(id);

            IUserDto userToReturn = new UserPostsDto(user);

            return userToReturn;
        }

        public UserSpecificPostDto CreateSpecificUserPost(int id, PostDto postDto)
        {
            var user = this.usersRepository.GetById(id);
            postDto.UserId = user.Id;
            var post = this.modelMapper.MapPostCreate(postDto);
            post.User = user;

            if (user.IsBlocked)
            {
                throw new BlockedUserException($"The user:{user.Username} is blocked and he can't create posts or comments.");
            }

            var createdPost = this.postsService.Create(post);
            var createdPostToReturn = this.modelMapper.MapPostCreate(createdPost, id);
            createdPostToReturn.User = this.usersRepository.GetById(id);
            createdPostToReturn.Id = createdPost.Id;

            return new UserSpecificPostDto(createdPostToReturn);
        }

        public UserSpecificPostDto GetUserSpecificPost(int userId, int postId)
        {
            var post = this.GetPostByUser(userId, postId);
            
            return new UserSpecificPostDto(post);
        }     

        public UserSpecificPostDto UpdateUserSpecificPost(int userId, int postId, PostDto postDto, User authUser)
        {
            var post = this.GetPostByUser(userId, postId);

            if (post.UserId != authUser.Id)
            {
                throw new UnauthorizedOperationException(ModifyPostErrorMessage);
            }

            postDto.UserId = userId;
            var postUpdatedPattern = this.modelMapper.MapPostCreate(postDto);
            postUpdatedPattern.Id = post.Id;

            var updatedPost = this.postsService.Update(post.Id, postDto/*postUpdatedPattern*/);
            var updatedPostToReturn = this.modelMapper.MapPostCreate(updatedPost, userId);
            updatedPostToReturn.User = this.usersRepository.GetById(userId);
            updatedPostToReturn.Id = post.Id;

            return new UserSpecificPostDto(updatedPostToReturn);
        }

        public void DeleteUserSpecificPost(int userId, int postId, User authUser)
        {
            var post = this.GetPostByUser(userId, postId);

            if (post.UserId != authUser.Id && !authUser.Role.Name.Equals("Admin"))
            {
                throw new UnauthorizedOperationException(DeletePostErrorMessage);
            }

            this.postsService.DeletePost(post.Id);
        }

        #endregion

        #region Post reactions

        // POST Reactions
        public List<UserPostReactionDto> GetUserSpecificPostReactions(int userId, int postId)
        {
            var post = this.GetPostByUser(userId, postId);
            var reactionsToPost = post.Reactions;

            if (reactionsToPost.Count <= 0)
            {
                throw new EntityNotFoundException(NoPostReactionsErrorMessage);
            }

            return reactionsToPost.Select(r => new UserPostReactionDto(r)).ToList();
        }

        public UserPostReactionDto CreateUserSpecificPostReaction(int userId, int postId, PostReactionDto postReactionDto, User author)
        {
            var post = this.GetPostByUser(userId, postId);
            PostReaction postReact = this.modelMapper.MapPostReactionCreate(post.Id, author, postReactionDto);
            var createdPostReaction = this.postsService.CreatePostReaction(post.Id, postReact);
            var createdPostReactionToReturn = this.modelMapper.MapPostReactionCreate(post.Id, author, createdPostReaction);
            createdPostReactionToReturn.Post = post;
            createdPostReactionToReturn.Id = createdPostReaction.Id;

            return new UserPostReactionDto(createdPostReactionToReturn);
        }

        public UserPostReactionDto GetUserPostSpecificReaction(int userId, int postId, int reactionId)
        {
            var post = this.GetPostByUser(userId, postId);
            var reaction = this.GetPostReactionInPost(post.Id, reactionId);

            return new UserPostReactionDto(reaction);
        }

        public UserPostReactionDto UpdateUserPostSpecificReaction(int userId, int postId, int reactionId, PostReactionDto postReactionDto, User authUser)
        {
            var post = this.GetPostByUser(userId, postId);
            var reaction = this.GetPostReactionInPost(post.Id, reactionId);
            PostReaction pr = this.modelMapper.MapPostReactionCreate(post.Id, authUser, postReactionDto);

            if (reaction.UserId != authUser.Id)
            {
                throw new UnauthorizedOperationException(ModifyPostReactionErrorMessage);
            }

            var updatedPr = this.postsService.UpdatePostReaction(post.Id, authUser.Username, pr);
            var createdPostReactionToReturn = this.modelMapper.MapPostReactionCreate(post.Id, authUser, updatedPr);
            createdPostReactionToReturn.Post = post;

            return new UserPostReactionDto(createdPostReactionToReturn);
        }

        public void DeleteUserPostSpecificReaction(int userId, int postId, int reactionId,  User authUser)
        {
            var post = this.GetPostByUser(userId, postId);
            var reaction = this.GetPostReactionInPost(post.Id, reactionId);

            if (reaction.UserId != authUser.Id && !authUser.Role.Name.Equals("Admin"))
            {
                throw new UnauthorizedOperationException(DeletePostReactionErrorMessage);
            }

            this.postsService.DeletePostReaction(post.Id, authUser);
        }
        #endregion

        #region Comments
        // Comments 
        public IUserDto GetSpecificUserComments(int id)
        {
            var user = this.usersRepository.GetById(id);

            IUserDto userToReturn = new UserCommentsDto(user);

            return userToReturn;
        }

        public CommentPostUserDto GetUserSpecificComment(int userId, int commentId)
        {
            var comment = this.GetCommentByUser(userId, commentId);
            return new CommentPostUserDto(comment);
        }

        public CommentPostUserDto UpdateUserSpecificComment(int userId, int commentId, CommentDto commentDto, User authUser)
        {
            var comment = this.GetCommentByUser(userId, commentId);

            if (comment.UserId != authUser.Id)
            {
                throw new UnauthorizedOperationException(ModifyCommentErrorMessage);
            }

            Comment commentFromDto = this.modelMapper.ToModel(commentDto, comment.PostId, authUser);
            var updatedComment = this.commentsService.Update(comment.PostId, comment.Id, commentFromDto, authUser.Id);
            var updatedCommentToReturn = this.modelMapper.ToModel(updatedComment, comment.PostId, authUser);
            updatedCommentToReturn.Post = comment.Post;

            return new CommentPostUserDto(updatedCommentToReturn);
        }

        public void DeleteUserSpecificComment(int userId, int commentId, User authUser)
        {
            var comment = this.GetCommentByUser(userId, commentId);

            if (comment.UserId != authUser.Id && !authUser.Role.Name.Equals("Admin"))
            {
                throw new UnauthorizedOperationException(DeleteCommentErrorMessage); // TODO
            }

            this.commentsService.Delete(comment.PostId, comment.Id, authUser);
        }
        #endregion

        #region Comment reaction
        // Comment reactions
        public List<UserCommentReactionDto> GetUserSpecificCommentReactions(int userId, int commentId)
        {
            var comment = this.GetCommentByUser(userId, commentId);
            var reactionsToComment = comment.CommentReactions;

            if (reactionsToComment.Count <= 0)
            {
                throw new EntityNotFoundException(NoCommentReactionsErrorMessage);
            }

            return reactionsToComment.Select(r => new UserCommentReactionDto(r)).OrderBy(r=>r.Id).ToList();
        }

        public UserCommentReactionDto CreateUserSpecificCommentReaction(int userId, int commentId, CommentReactionDto commentReactionDto, User author)
        {
            var comment = this.GetCommentByUser(userId, commentId);
            CommentReaction commentReact = this.modelMapper.ToModel(commentReactionDto, comment.Id, author);
            var createdPostReaction = this.commentsService.CreateCommentReaction(comment.PostId, comment.Id, author.Id, commentReact);
            var createdPostReactionToReturn = this.modelMapper.ToModel(createdPostReaction, comment.Id, author);

            return new UserCommentReactionDto(createdPostReactionToReturn);
        }

        public UserCommentReactionDto GetUserCommentSpecificReaction(int userId, int commentId, int reactionId)
        {
            var comment = this.GetCommentByUser(userId, commentId);
            var reaction = this.GetCommentReactionInComment(comment.Id, reactionId);

            return new UserCommentReactionDto(reaction);
        }

        public UserCommentReactionDto UpdateUserCommentSpecificReaction(int userId, int commentId, int reactionId, CommentReactionDto commentReactionDto, User authUser)
        {
            var comment = this.GetCommentByUser(userId, commentId);
            var reaction = this.GetCommentReactionInComment(comment.Id, reactionId);
            CommentReaction cr = this.modelMapper.ToModel(commentReactionDto, comment.Id, authUser);

            if (reaction.UserId != authUser.Id)
            {
                throw new UnauthorizedOperationException(ModifyCommentReactionErrorMessage);
            }

            var updatedCR = this.commentsService.UpdateCommentReaction(comment.PostId, comment.Id, reaction.Id, cr, authUser.Id);
            var updatedCRToReturn = this.modelMapper.ToModel(updatedCR, comment.Id, authUser);

            return new UserCommentReactionDto(updatedCRToReturn);
        }

        public void DeleteUserCommentSpecificReaction(int userId, int commentId, int reactionId, User authUser)
        {
            var comment = this.GetCommentByUser(userId, commentId);
            var reaction = this.GetCommentReactionInComment(comment.Id, reactionId);

            if (reaction.UserId != authUser.Id && !authUser.Role.Name.Equals("Admin"))
            {
                throw new UnauthorizedOperationException(DeletePostReactionErrorMessage);
            }

            this.commentsService.DeleteCommentReaction(comment.PostId, comment.Id, reactionId, authUser.Id);
        }
        #endregion

        #region Post and Comments
        // Post and Comments
        public List<PostSpecificCommentDto> GetUserPostAllComments(int userId, int postId)
        {
            var post = this.GetPostByUser(userId, postId);
            var comments = this.commentsRepository.GetCommentByPostId(post.Id).Select(c => new PostSpecificCommentDto(c)).ToList();

            return comments;
        }

        public PostSpecificCommentDto CreateUserPostAllComment(int userId, int postId, CommentDto commentDto, User authUser) 
        {
            var post = this.GetPostByUser(userId, postId);
            var comment = this.modelMapper.ToModel(commentDto);
            comment.PostId = post.Id;
            comment.User = authUser;

            if (authUser.IsBlocked)
            {
                throw new BlockedUserException($"The user:{authUser.Username} is blocked and he can't create posts or comments.");
            }

            var createdComment = this.commentsService.Create(comment);
            var createdCommentToReturn = this.modelMapper.ToModel(createdComment, post.Id, authUser);

            return new PostSpecificCommentDto(createdCommentToReturn);
        }

        public PostSpecificCommentDto GetUserPostSpecificComment(int userId, int postId, int commentId)
        {
            var post = this.GetPostByUser(userId, postId);
            var comment = this.GetCommentInPost(post.Id, commentId);

            return new PostSpecificCommentDto(comment);
        }

        public PostSpecificCommentDto UpdateUserPostSpecificComment(int userId, int postId, int commentId, User authUser, CommentDto commentDto)
        {
            var post = this.GetPostByUser(userId, postId);
            var comment = this.GetCommentInPost(post.Id, commentId);

            if (comment.UserId != authUser.Id)
            {
                throw new UnauthorizedOperationException(ModifyCommentErrorMessage);
            }

            Comment commentFromDto = this.modelMapper.ToModel(commentDto, post.Id, authUser);

            var updatedComment = this.commentsService.Update(comment.PostId, comment.Id, commentFromDto ,authUser.Id);
            var updatedCommentToReturn = this.modelMapper.ToModel(updatedComment, post.Id, authUser);

            return new PostSpecificCommentDto(updatedCommentToReturn);
        }

        public void DeleteUserPostSpecificComment(int userId, int postId, int commentId, User authUser)
        {
            var post = this.GetPostByUser(userId, postId);
            var comment = this.GetCommentInPost(post.Id, commentId);

            if (comment.UserId != authUser.Id && !authUser.Role.Name.Equals("Admin"))
            {
                throw new UnauthorizedOperationException(DeleteCommentErrorMessage);
            }

            this.commentsService.Delete(post.Id, comment.Id, authUser);
        }
        #endregion

        #region Helper Methods
        private Post GetPostByUser(int userId, int postId)
        {
            var user = this.usersRepository.GetById(userId);
            var posts = this.postsRepository.GetAll().Where(p => p.UserId == userId).OrderBy(p => p.Id);
            var postToReturn = posts.Where(p => p.Id == postId).FirstOrDefault();

            if (postToReturn == null)
            {
                throw new EntityNotFoundException($"Post with id:{postId} for user:{user.Username} does not exist");
            }

            return postToReturn;
        }

        private Comment GetCommentByUser(int userId, int commentId)
        {
            var user = this.usersRepository.GetById(userId);
            var comments = this.commentsRepository.GetAll().Where(user => user.Id == userId).OrderBy(c => c.Id).ToList();
            var commentToReturn = comments.Where(c => c.Id == commentId).FirstOrDefault();

            if (commentToReturn == null)
            {
                throw new EntityNotFoundException($"Comment with id:{commentId} for user:{user.Username} does not exist");
            }

            return commentToReturn;
        }

        private Comment GetCommentInPost(int postId, int commentId)
        {
            var post = this.postsRepository.GetById(postId);
            var comments = post.Comments.OrderBy(c => c.Id).ToList();
            var commentToReturn = comments.Where(c => c.Id == commentId).FirstOrDefault();

            if (commentToReturn == null)
            {
                throw new EntityNotFoundException($"Comment with id:{commentId} for post with id:{postId} does not exist");
            }

            return commentToReturn;
        }

        private PostReaction GetPostReactionInPost(int postId, int reactionId)
        {
            var post = this.postsRepository.GetById(postId);
            var reactions = post.Reactions.OrderBy(c => c.Id).ToList();
            var reactionToReturn = reactions.Where(r => r.Id == reactionId).FirstOrDefault();

            if (reactionToReturn == null)
            {
                throw new EntityNotFoundException($"Reaction with id:{reactionId} for post with id:{postId} does not exist");
            }

            return reactionToReturn;
        }

        private CommentReaction GetCommentReactionInComment(int commentId, int reactionId)
        {
            var comment = this.commentsRepository.GetCommentById(commentId);
            var reactions = comment.CommentReactions.OrderBy(c => c.Id).ToList();
            var reactionToReturn = reactions.Where(r => r.Id == reactionId).FirstOrDefault();

            if (reactionToReturn == null)
            {
                throw new EntityNotFoundException($"Reaction with id:{reactionId} for comment with id:{commentId} does not exist");
            }

            return reactionToReturn;
        }

        public bool UsernameExists(string username)
        {
            bool usernameExists = true;

            try
            {
                _ = this.usersRepository.GetByUsername(username);
            }
            catch (EntityNotFoundException)
            {
                usernameExists = false;
            }

            return usernameExists;
        }

        public bool EmailExists(string email)
        {
            bool emailExists = true;

            try
            {
                _ = this.usersRepository.GetByEmail(email);
            }
            catch (EntityNotFoundException)
            {
                emailExists = false;
            }

            return emailExists;
        }

        #endregion
    }
}
