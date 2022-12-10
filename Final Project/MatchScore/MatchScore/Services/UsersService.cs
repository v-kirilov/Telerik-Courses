using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.QueryParameters;
using MatchScore.Repositories.Contracts;
using MatchScore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchScore.Services
{
    public class UsersService : IUsersService
    {
        private const string ModifyRoleErrorMessage = "Only the admin can update this user's role.";
        private const string ModifyPasswordErrorMessage = "Only the user himself can update this user's password.";
        private const string ModifyUserErrorMessage = "Only the user himself or the admin can update this user's info.";
        private const string DuplicateEmailErrorMessage = "This email already exists. Try with different email.";
        private const string DeleteUserErrorMessage = "Only the admin can delete users.";

        private readonly IUsersRepository usersRepository;
        private readonly IModelMapper modelMapper;
        private readonly IPlayersRepository playersRepository;

        public UsersService(IUsersRepository usersRepository, IModelMapper modelMapper, IPlayersRepository playersRepository)
        {
            this.usersRepository = usersRepository;
            this.modelMapper = modelMapper;
            this.playersRepository = playersRepository;
        }

        public List<UserDto> GetAll()
        {
            var users = this.usersRepository.GetAll();
            var usersToReturn = users.Select(u => this.modelMapper.MapUserToDto(u))
                                    .ToList();

            return usersToReturn;
        }

        public UserDto GetById(int id)
        {
            var user = this.usersRepository.GetById(id);
            var userToReturn = this.modelMapper.MapUserToDto(user);

            return userToReturn;
        }

        public UserDto GetByEmail(string email)
        {
            var user = this.usersRepository.GetByEmail(email);
            var userToReturn = this.modelMapper.MapUserToDto(user);

            return userToReturn;
        }

        public PaginatedList<UserDto> FilterBy(UserQueryParameters filterParameters)
        {
            var users = this.usersRepository.FilterBy(filterParameters);
            var usersList = users.Select(u => this.modelMapper.MapUserToDto(u)).ToList();
            var usersToReturn = new PaginatedList<UserDto>(usersList, users.TotalPages, users.PageNumber);

            return usersToReturn;
        }

        public UserDto Create(User user)
        {
            EmailExists(user);
            EncodePassword(user);

            User createdUser = this.usersRepository.Create(user);
            var userToReturn = this.modelMapper.MapUserToDto(createdUser);

            return userToReturn;
        }

        public UserDto Update(int id, User userIncoming, User authUser)
        {
            User userToUpdate = this.usersRepository.GetById(id);

            if (!userToUpdate.Id.Equals(authUser.Id) && !authUser.Role.Name.Equals("Admin"))
            {
                throw new UnauthorizedOperationException(ModifyUserErrorMessage);
            }

            //userToUpdate = UpdateEmail(id, userIncoming, userToUpdate);
            userToUpdate = UpdatePassword(id, userIncoming, authUser, userToUpdate);
            userToUpdate = UpdateRole(id, userIncoming, authUser, userToUpdate);

            var userToReturn = this.modelMapper.MapUserToDto(userToUpdate);

            return userToReturn;
        }

        public void Delete(int id, User authUser)
        {
            User userToDelete = this.usersRepository.GetById(id);

            if (userToDelete.Role.Name.Equals("Verified"))
            {
                var player = userToDelete.Player;
                player.User = null;
            }

            if (!authUser.Role.Name.Equals("Admin"))
            {
                throw new UnauthorizedOperationException(DeleteUserErrorMessage);
            }

            userToDelete.Requests.Clear();

            this.usersRepository.Delete(id);
        }

        #region Helpers
        private void EmailExists(User user)
        {
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

        private User UpdatePassword(int id, User userIncoming, User authUser, User userToUpdate)
        {
            if (!string.IsNullOrEmpty(userIncoming.Password))
            {
                if (userToUpdate.Id.Equals(authUser.Id))
                {
                    EncodePassword(userIncoming);
                    userToUpdate = this.usersRepository.UpdatePassword(id, userIncoming.Password);
                }
                else
                {
                    throw new UnauthorizedOperationException(ModifyPasswordErrorMessage);
                }
            }

            return userToUpdate;
        }

        private User UpdateRole(int id, User userIncoming, User authUser, User userToUpdate)
        {
            if (userIncoming.Role == null)
            {
                return userToUpdate;
            }

            if (userToUpdate.Role.Name != userIncoming.Role.Name)
            {
                if (authUser.Role.Name.Equals("Admin"))
                {
                    NullifyAssociatedPlayer(userIncoming, userToUpdate);
                    userToUpdate = this.usersRepository.UpdateRole(id, userIncoming.Role.Name);
                }
                else
                {
                    throw new UnauthorizedOperationException(ModifyRoleErrorMessage);
                }
            }

            return userToUpdate;
        }

        private void NullifyAssociatedPlayer(User userIncoming, User userToUpdate)
        {
            if (userToUpdate.Role.Name == "Verified" && userIncoming.Role.Name == "Default")
            {
                var player = this.playersRepository.GetById(userToUpdate.Player.Id);
                player.UserId = null;
                player.User = null;
            }
        }


        private static void EncodePassword(User user)
        {
            var password = user.Password;
            var encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            user.Password = encodedPassword;
        }

        #endregion
    }
}
