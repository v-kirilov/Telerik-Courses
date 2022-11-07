using ForumSystem.Exceptions;
using ForumSystem.Models;
using ForumSystem.Models.Enums;
using ForumSystem.Models.DTO;
using ForumSystem.Repositories.Contracts;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using Humanizer;
using System.Collections.Generic;
using System.ComponentModel.Design;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models.Users.UsersViewModels;

namespace ForumSystem.Helpers
{
    public class ModelMapper : IModelMapper
    {
        private readonly IRolesRepository rolesRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IPhoneNumbersRepository phoneNumbersRepository;

        public ModelMapper(IRolesRepository rolesRepository, IUsersRepository usersRepository, IPhoneNumbersRepository phoneNumbersRepository)
        {
            this.rolesRepository = rolesRepository;
            this.usersRepository = usersRepository;
            this.phoneNumbersRepository = phoneNumbersRepository;
        }

        public Comment ToModel(CommentDto commentModel)
        {
            return new Comment
            {   
                Id = commentModel.CommentId,
                CommentContent = commentModel.CommentContent
            };
        }

        public Comment ToModel(CommentDto commentModel, int postId, User user)
        {
            return new Comment
            {
                Id = commentModel.CommentId,
                CommentContent = commentModel.CommentContent,
                UserId = user.Id,
                User = user,
                PostId = postId
            };
        }

        public User MapUserCreate(CreateUserDto dto)
        {
            return new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Username = dto.Username,
                Password = dto.Password
            };
        }

        public User MapUserUpdate(int id, UpdateUserDto dto, User authUser)
        {
            var user = this.usersRepository.GetById(id);

            var updatedUser = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password,
                Username = dto.Username,
                ProfilePicture = dto.ProfilePicture
            };

            

            if (!string.IsNullOrEmpty(dto.Role))
            {
                updatedUser.Role = this.rolesRepository.GetByName(dto.Role);
            }
            else
            {
                updatedUser.Role = user.Role;
            }

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                if (authUser.Role.Name != "Admin")
                {
                    throw new UnauthorizedOperationException("Only admins can add phone numbers.");
                }
                if (updatedUser.Role.Name != "Admin")
                {
                    throw new UnauthorizedOperationException("Only admins can have a phone number.");
                }

                var phoneNumber = this.phoneNumbersRepository.Create(new PhoneNumber() { Number = dto.PhoneNumber, User = user, UserId = user.Id });
                updatedUser.PhoneNumber = phoneNumber;
            }

            if (!dto.IsBlocked.HasValue)
            {
                dto.IsBlocked = user.IsBlocked;
                updatedUser.IsBlocked = (bool)dto.IsBlocked;
            }
            else
            {
                updatedUser.IsBlocked = (bool)dto.IsBlocked;
            }

            return updatedUser;
        }

        public UpdateUserDto MapUserEditView(EditViewModel editViewModel, string uniqueFileName)
        {
            return new UpdateUserDto()
            {
                FirstName = editViewModel.FirstName,
                LastName = editViewModel.LastName,
                Email = editViewModel.Email,
                Username = editViewModel.Username,
                Password = editViewModel.Password,
                Role = editViewModel.Role,
                PhoneNumber = editViewModel.PhoneNumber,
                IsBlocked = editViewModel.IsBlocked,
                ProfilePicture = uniqueFileName
            };
        }

        public CreateUserDto MapUserRegView(RegisterViewModel regViewModel)
        {
            return new CreateUserDto()
            {
                FirstName = regViewModel.FirstName,
                LastName = regViewModel.LastName,
                Username = regViewModel.Username,
                Email = regViewModel.Username,
                Password = regViewModel.Password
            };
        }
        public PostDto MapPost(Post post)
        {
            return new PostDto
            {
                Id=post.Id,
                User = post.User.Username,
                UserId = post.UserId,
                Title = post.Title,
                Content = post.Content,
                comments = post.Comments
                    .Select(x => ToDto(x))
                    .ToList(),
                reactions = post.Reactions
                    .Select(r => MapPostReaction(r))
                    .ToList()
            };
        }
        public Post MapPostCreate(PostDto dto)
        {
            if (dto.UserId == 0)
            {
                throw new EntityNotFoundException();
            }
            return new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = dto.UserId,
            };
        }
        public Post MapPostCreate(PostDto dto,int userId)
        {
            
            return new Post
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = userId,
            };
        }
        public PostReaction MapPostReactionCreate(int postId, User author, PostReactionDto dto)
        {
            string reaction = dto.Reaction.ToLower();
            if (reaction != "like" && reaction != "dislike")
            {
                throw new InvalidDataException("Invalid reaction, must be Like or Dislike");
            }
            var react = (Reactions)Enum.Parse(typeof(Reactions), dto.Reaction, true);
            return new PostReaction
            {
                UserId = author.Id,
                User = author,
                Reaction = react,
                PostId = postId
            };
        }

        public CommentReaction ToModel(CommentReactionDto commentReactionDto, int commentId, User authUser)
        {
            string reaction = commentReactionDto.Reaction.ToLower();
            if (reaction != "like" && reaction != "dislike")
            {
                throw new InvalidDataException("Invalid reaction, must be Like or Dislike");
            }
            var react = (Reactions)Enum.Parse(typeof(Reactions), commentReactionDto.Reaction, true);
            return new CommentReaction
            {
                Id = commentReactionDto.Id,
                CommentId = commentId,
                User = authUser,
                UserId = authUser.Id,
                Reaction = react,
            };
        }

        public PhoneNumber ToModel(PhoneNumberDto phoneNumberDto, int userId)
        {
            return new PhoneNumber
            {
                Number = phoneNumberDto.Number,
                UserId = userId
            };
        }

        public PhoneNumberDto ToDto(PhoneNumber phoneNumberModel)
        {
            return new PhoneNumberDto
            {
                Id = phoneNumberModel.Id,
                Number = phoneNumberModel.Number,
                Username = phoneNumberModel.User.Username
            };
        }
        public CommentDto ToDto(Comment comment)
        {
            return new CommentDto
            {
                CommentId = comment.Id,
                CommentContent = comment.CommentContent,
                Author = comment.User.Username,    
                PostId = comment.PostId,
                CommentReactions = comment.CommentReactions.Select(r => CommentReactionMap(r)).ToList() /*?? new List<CommentReactionDto>()*/
            };
        }
        public PostReactionDto MapPostReaction(PostReaction postReaction)
        {
            return new PostReactionDto
            {
                Id = postReaction.Id,
                Author = postReaction.User.Username,
                Reaction = postReaction.Reaction.ToString()
            };
        }
        public CommentReactionDto CommentReactionMap(CommentReaction commentReaction)
        {
            return new CommentReactionDto
            {
                Id = commentReaction.Id,
                Author = commentReaction.User.Username,
                Reaction = commentReaction.Reaction.ToString()
            };

        }

        public PostMVCDto ToMVCDto(PostDto post)
        {
            return new PostMVCDto
            {
                Id = post.Id,
                User = post.User,
                UserId = post.UserId,
                Title = post.Title,
                Content = post.Content,
                comments = post.comments,
                reactions = post.reactions,
                Comment = new CommentDto()
            };
        }
    }
}

