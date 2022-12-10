using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.QueryParameters;
using MatchScore.Repositories.Contracts;
using MatchScore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchScore.Services
{
    public class PlayersService : IPlayersService
    {
        private const string CreatePlayerErrorMessage = "Only the admin or directors can create a new player.";
        private const string ModifyPlayerVerificationErrorMessage = "Only the admin can verify users.";
        private const string ModifyPlayerErrorMessage = "Only the player himself, admin or directors can update this player's info.";
        private const string ModifyVerifiedPlayerErrorMessage = "Only the player himself or the admin can update this player's info.";
        private const string DuplicateFullNameErrorMessage = "This player already exists.";
        private const string DeletePlayerErrorMessage = "Only the the admin can delete players.";
        private const string DeletePlayerWithEventsErrorMessage = "This player cannot be deleted because he participated in tournaments and/or matches";

        private readonly IPlayersRepository playersRepository;
        private readonly IModelMapper modelMapper;

        public PlayersService(IPlayersRepository playersRepository, IModelMapper modelMapper)
        {
            this.playersRepository = playersRepository;
            this.modelMapper = modelMapper;
        }

        public List<PlayerDto> GetAll()
        {
            var players = this.playersRepository.GetAll();
            var playerToReturn = players.Select(p => this.modelMapper.MapPlayerToDto(p))
                                        .ToList();

            return playerToReturn;
        }

        public PlayerDto GetById(int id)
        {
            var player = this.playersRepository.GetById(id);
            var playerToReturn = this.modelMapper.MapPlayerToDto(player);

            return playerToReturn;
        }

        public PlayerDto GetByFullName(string fullName)
        {
            var player = this.playersRepository.GetByFullName(fullName);
            var playerToReturn = this.modelMapper.MapPlayerToDto(player);

            return playerToReturn;
        }

        public PaginatedList<PlayerDto> FilterBy(PlayerQueryParameters filterParameters)
        {
            var players = this.playersRepository.FilterBy(filterParameters);
            var playersList = players.Select(p => this.modelMapper.MapPlayerToDto(p)).ToList();
            var playersToReturn = new PaginatedList<PlayerDto>(playersList, players.TotalPages, players.PageNumber);

            return playersToReturn;
        }

        public PlayerDto Create(Player player, User authUser)
        {
            if (!authUser.Role.Name.Equals("Director") && !authUser.Role.Name.Equals("Admin"))
            {
                throw new UnauthorizedOperationException(CreatePlayerErrorMessage);
            }

            FullNameExists(player);

            Player createdPlayer = this.playersRepository.Create(player);
            var playerToReturn = this.modelMapper.MapPlayerToDto(player);

            return playerToReturn;
        }

        public PlayerDto Update(int id, Player playerIncoming, User authUser)
        {
            Player playerToUpdate = this.playersRepository.GetById(id);

            if (authUser.Role.Name.Equals("Director") && playerToUpdate.User != null)
            {
                throw new UnauthorizedOperationException(ModifyVerifiedPlayerErrorMessage);
            }

            if (!playerToUpdate.UserId.Equals(authUser.Id) && !authUser.Role.Name.Equals("Admin") && !authUser.Role.Name.Equals("Director"))
            {
                throw new UnauthorizedOperationException(ModifyPlayerErrorMessage);
            }

            playerToUpdate = UpdateFullName(id, playerIncoming, playerToUpdate);
            playerToUpdate = UpdateCountry(id, playerIncoming, playerToUpdate);
            playerToUpdate = UpdateSportClub(id, playerIncoming, playerToUpdate);
            playerToUpdate = UpdateVerificationUser(id, playerIncoming, playerToUpdate, authUser);
            var playerToReturn = this.modelMapper.MapPlayerToDto(playerToUpdate);

            return playerToReturn;
        }

        public void Delete(int id, User authUser)
        {
            Player playerToDelete = this.playersRepository.GetById(id);

            if (playerToDelete.Tournaments.Any() || playerToDelete.Scores.Any())
            {
                throw new InvalidOperationException(DeletePlayerWithEventsErrorMessage);
            }

            if (!authUser.Role.Name.Equals("Admin"))
            {
                throw new UnauthorizedOperationException(DeletePlayerErrorMessage);
            }

            this.playersRepository.Delete(id);
        }

        #region Helpers
        public bool FullNameExists(string fullName)
        {
            bool duplicateExists = true;

            try
            {
                this.playersRepository.GetByFullName(fullName);
            }
            catch (EntityNotFoundException)
            {
                duplicateExists = false;
            }

            return duplicateExists;
        }
        private void FullNameExists(Player player)
        {
            bool duplicateExists = this.FullNameExists(player.FullName);

            if (duplicateExists)
            {
                throw new DuplicateEntityException(DuplicateFullNameErrorMessage);
            }
        }

        private Player UpdateCountry(int id, Player playerIncoming, Player playerToUpdate)
        {
            if (playerIncoming.Country != null)
            {
                playerToUpdate = this.playersRepository.UpdateCountry(id, playerIncoming.Country.Name);
            }

            return playerToUpdate;
        }

        private Player UpdateFullName(int id, Player playerIncoming, Player playerToUpdate)
        {
            if (!string.IsNullOrEmpty(playerIncoming.FullName) && playerIncoming.FullName != playerToUpdate.FullName)
            {
                this.FullNameExists(playerIncoming);

                playerToUpdate = this.playersRepository.UpdateFullName(id, playerIncoming.FullName);
            }

            return playerToUpdate;
        }

        private Player UpdateSportClub(int id, Player playerIncoming, Player playerToUpdate)
        {
            if (playerIncoming.SportClub != null)
            {
                playerToUpdate = this.playersRepository.UpdateSportClub(id, playerIncoming.SportClub.Name);
            }

            return playerToUpdate;
        }

        private Player UpdateVerificationUser(int id, Player playerIncoming, Player playerToUpdate, User authUser)
        {
          
            if (playerIncoming.User != null)
            {
                if (!authUser.Role.Name.Equals("Admin"))
                {
                    throw new UnauthorizedOperationException(ModifyPlayerVerificationErrorMessage);
                }

                playerToUpdate = this.playersRepository.UpdateUser(id, playerIncoming.User.Email);
            }

            return playerToUpdate;
        }
        #endregion
    }
}
