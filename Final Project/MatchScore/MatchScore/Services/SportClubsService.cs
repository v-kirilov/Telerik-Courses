using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Repositories.Contracts;
using MatchScore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Services
{
    public class SportClubsService : ISportClubsService
    {
        private const string DuplicateNameErrorMessage = "This club already exists.";
        private const string ModifyNameErrorMessage = "Only the admin can update this teams's name.";
        private const string DeleteTeamErrorMessage = "Only the admin can delete teams.";

        private readonly ISportClubsRepository sportClubsRepository;
        private readonly IModelMapper modelMapper;

        public SportClubsService(ISportClubsRepository sportClubsRepository, IModelMapper modelMapper)
        {
            this.sportClubsRepository = sportClubsRepository;
            this.modelMapper = modelMapper;
        }

        public List<SportClub> GetAll()
        {
            var sportClubs = this.sportClubsRepository.GetAll();
            return sportClubs;
        }

        public SportClub GetById(int id)
        {
            var sportClub = this.sportClubsRepository.GetById(id);
            return sportClub;
        }

        public SportClub GetByName(string name)
        {
            var sportClub = this.sportClubsRepository.GetByName(name);
            return sportClub;
        }

        public SportClub Create(SportClub sportClub)
        {
            //var sportClub = this.modelMapper.MapDtoToSportClub(sportClubDto);
            bool duplicateExists = SportClubExists(sportClub.Name);

            if (duplicateExists)
            {
                throw new DuplicateEntityException(DuplicateNameErrorMessage);
            }

            var createdSportClub = this.sportClubsRepository.Create(sportClub);

            return createdSportClub;
        }

        public SportClub Update(int id, SportClub sportClubIncoming, User authUser)
        {
            var sportClubToUpdate = this.GetById(id);
            //var sportClubIncoming = this.modelMapper.MapDtoToSportClub(sportClubDto);
            

            if (authUser.Role.Name != "Admin")
            {
                throw new UnauthorizedOperationException(ModifyNameErrorMessage);
            }

            bool duplicateExists = SportClubExists(sportClubIncoming.Name);

            if (duplicateExists)
            {
                throw new DuplicateEntityException(DuplicateNameErrorMessage);
            }

            var updatedSportClub = this.sportClubsRepository.UpdateName(id, sportClubIncoming.Name);

            return updatedSportClub;
        }

        public void Delete(int id, User authUser)
        {
            if (authUser.Role.Name != "Admin")
            {
                throw new UnauthorizedOperationException(DeleteTeamErrorMessage);
            }

            this.sportClubsRepository.Delete(id);
        }

        public bool SportClubExists(string sportClubName)
        {
            bool duplicateExists = true;

            try
            {
                this.sportClubsRepository.GetByName(sportClubName);
            }
            catch (EntityNotFoundException)
            {
                duplicateExists = false;
            }

            return duplicateExists;
        }
    }
}
