using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.DTO.Users;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services;
using ForumSystem.Services.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Tests
{
    class TestUserHelper
    {
        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock)
        {
            var repoCommentsMock = new Mock<ICommentsRepository>();
            var repoPostsMock = new Mock<IPostsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var serviceCommentsMock = new Mock<ICommentsService>();
            var servicePostsMock = new Mock<IPostsService>();
            var modelMapperMock = new Mock<IModelMapper>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<IModelMapper> modelMapperMock)
        {
            var repoCommentsMock = new Mock<ICommentsRepository>();
            var repoPostsMock = new Mock<IPostsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var serviceCommentsMock = new Mock<ICommentsService>();
            var servicePostsMock = new Mock<IPostsService>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<ICommentsRepository> repoCommentsMock)
        {
            var repoPostsMock = new Mock<IPostsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var serviceCommentsMock = new Mock<ICommentsService>();
            var servicePostsMock = new Mock<IPostsService>();
            var modelMapperMock = new Mock<IModelMapper>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<ICommentsRepository> repoCommentsMock,
                                                          Mock<IPostsRepository> repoPostsMock)
        {
            var repoRoleMock = new Mock<IRolesRepository>();

            var serviceCommentsMock = new Mock<ICommentsService>();
            var servicePostsMock = new Mock<IPostsService>();
            var modelMapperMock = new Mock<IModelMapper>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<ICommentsRepository> repoCommentsMock,
                                                          Mock<ICommentsService> serviceCommentsMock)
        {
            var repoPostsMock = new Mock<IPostsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var servicePostsMock = new Mock<IPostsService>();
            var modelMapperMock = new Mock<IModelMapper>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<ICommentsRepository> repoCommentsMock,
                                                          Mock<ICommentsService> serviceCommentsMock, Mock<IModelMapper> modelMapperMock)
        {
            var repoPostsMock = new Mock<IPostsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var servicePostsMock = new Mock<IPostsService>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<IPostsRepository> repoPostsMock)
        {
            var repoCommentsMock = new Mock<ICommentsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var serviceCommentsMock = new Mock<ICommentsService>();
            var servicePostsMock = new Mock<IPostsService>();
            var modelMapperMock = new Mock<IModelMapper>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<IPostsRepository> repoPostsMock,
                                                          Mock<IPostsService> servicePostsMock, Mock<IModelMapper> modelMapperMock)
        {
            var repoCommentsMock = new Mock<ICommentsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var serviceCommentsMock = new Mock<ICommentsService>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<IPostsRepository> repoPostsMock,
                                                          Mock<ICommentsService> serviceCommentsMock)
        {
            var repoCommentsMock = new Mock<ICommentsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var servicePostsMock = new Mock<IPostsService>();
            var modelMapperMock = new Mock<IModelMapper>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<IPostsRepository> repoPostsMock,
                                                          Mock<IModelMapper> modelMapperMock, Mock<ICommentsService> serviceCommentsMock)
        {
            var repoCommentsMock = new Mock<ICommentsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var servicePostsMock = new Mock<IPostsService>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<IPostsRepository> repoPostsMock,
                                                          Mock<IPostsService> servicePostsMock)
        {
            var repoCommentsMock = new Mock<ICommentsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var serviceCommentsMock = new Mock<ICommentsService>();
            var modelMapperMock = new Mock<IModelMapper>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }
        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<IPostsRepository> repoPostsMock,
                                                          Mock<IPostsService> servicePostsMock, Mock<ICommentsRepository> repoCommentsMock)
        {
            var repoRoleMock = new Mock<IRolesRepository>();

            var serviceCommentsMock = new Mock<ICommentsService>();
            var modelMapperMock = new Mock<IModelMapper>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<IPostsService> servicePostsMock, 
                                                          Mock<IModelMapper> modelMapperMock)
        {
            var repoCommentsMock = new Mock<ICommentsRepository>();
            var repoPostsMock = new Mock<IPostsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var serviceCommentsMock = new Mock<ICommentsService>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static UsersService InitializeUsersService(Mock<IUsersRepository> repoUsersMock, Mock<IPostsService> servicePostsMock,
                                                          Mock<IModelMapper> modelMapperMock, Mock<IPostsRepository> repoPostsMock)
        {
            var repoCommentsMock = new Mock<ICommentsRepository>();
            var repoRoleMock = new Mock<IRolesRepository>();

            var serviceCommentsMock = new Mock<ICommentsService>();

            var sut = new UsersService(repoUsersMock.Object, servicePostsMock.Object, modelMapperMock.Object,
                                       repoPostsMock.Object, serviceCommentsMock.Object, repoCommentsMock.Object);
            return sut;
        }

        public static User GetTestDefaultUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "FirstNameTest",
                LastName = "LastNameTest",
                Email = "EmailTest",
                Username = "UsernameTest",
                IsBlocked = false,
                RoleId = 2,
                Role = GetTestDefaultRole()
            };
        }

        public static UpdateUserDto GetTestDefaultUpdateUserDto()
        {
            return new UpdateUserDto
            {
                FirstName = "FirstNameTest",
                LastName = "LastNameTest",
                Email = "EmailTest",
                Username = "UsernameTest",
                IsBlocked = false,
                Role = GetTestDefaultRole().Name
            };
        }

        public static CreateUserDto GetTestDefaultCreateUserDto()
        {
            return new CreateUserDto
            {
                FirstName = "FirstNameTest",
                LastName = "LastNameTest",
                Email = "EmailTest",
                Username = "UsernameTest"
            };
        }

        public static User GetTestAdminUser()
        {
            return new User
            {
                Id = 2,
                FirstName = "AdminFirstNameTest",
                LastName = "AdminLastNameTest",
                Email = "AdminEmailTest",
                Username = "AdminUserameTest",
                IsBlocked = false,
                RoleId = 1,
                Role = GetTestAdminRole()
            };
        }
        public static Role GetTestAdminRole()
        {
            return new Role
            {
                Id = 1,
                Name = "Admin"
            };
        }

        public static Role GetTestDefaultRole()
        {
            return new Role
            {
                Id = 2,
                Name = "Default"
            };
        }

        public static UserQueryParameters GetTestUserQueryParameters()
        {
            return new UserQueryParameters
            {
                Username = "Test",
                Email = "Test",
                FirstName = "Test"
            };
        }

        public static PhoneNumber GetTestPhoneNumber()
        {
            return new PhoneNumber
            {
                Id = 1,
                Number = "1111"
            };
        }

        public static Post GetTestPost()
        {
            return new Post
            {
                Id = 1,
                Title = "TestTitle",
                Content = "TestContent"
            };
        }

        public static PostDto GetTestPostDto()
        {
            return new PostDto
            {
                Id = 1,
                Title = "TestPostDtoTitle",
                Content = "TestPostDtoContent"
            };
        }

        public static UserSpecificPostDto GetTestUserSpecificPostDto()
        {
            return new UserSpecificPostDto(GetTestPost());
        }

        public static Comment GetTestComment()
        {
            return new Comment()
            {
                Id = 1,
                CommentContent = "TestContent"
            };
        }

        public static PostReaction GetTestPostReaction()
        {
            return new PostReaction()
            {
                Id = 1,
                Reaction = Models.Enums.Reactions.Like
            };
        }

        public static CommentReaction GetTestCommentReaction()
        {
            return new CommentReaction()
            {
                Id = 1,
                Reaction = Models.Enums.Reactions.Like
            };
        }
    }
}
