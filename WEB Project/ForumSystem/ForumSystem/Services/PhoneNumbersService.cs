using ForumSystem.Exceptions;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Repositories.Contracts;
using ForumSystem.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ForumSystem.Services
{
    public class PhoneNumbersService : IPhoneNumbersService
    {
        private readonly IPhoneNumbersRepository phoneNumbersRepository;
        private readonly IModelMapper modelMapper;

        public PhoneNumbersService(IPhoneNumbersRepository phoneNumbersRepository, IModelMapper modelMapper)
        {
            this.phoneNumbersRepository = phoneNumbersRepository;
            this.modelMapper = modelMapper;
        }

        public List<PhoneNumberDto> GetAll()
        {
            return this.phoneNumbersRepository.GetAll()
                                              .Select(c => this.modelMapper.ToDto(c))
                                              .ToList();
        }

        public PhoneNumberDto GetByUserId(int userId)
        {
            return this.modelMapper.ToDto(this.phoneNumbersRepository.GetByUserId(userId));
                                              
                                              
        }

        public PhoneNumberDto GetById(int userId, int phoneNumberId)
        {
            return this.modelMapper.ToDto(this.phoneNumbersRepository.GetById(userId, phoneNumberId));
        }


        public PhoneNumberDto Create(PhoneNumber phoneNumber, User user)
        {
            try
            {
                if (!user.Role.Name.Equals("Admin"))
                {
                    throw new UnauthorizedOperationException("You are not authorized to add a phone number!");
                }
                return this.modelMapper.ToDto(this.phoneNumbersRepository.Create(phoneNumber));
            }
            catch (UnauthorizedOperationException)
            {
                throw;
            }
            
        }

        public PhoneNumberDto Update(int userId, int phoneNumberId, PhoneNumber phoneNumber, User user)
        {
            try
            {
                if (!user.Role.Name.Equals("Admin"))
                {
                    throw new UnauthorizedOperationException("You are not authorized to update a phone number!");
                }
                return this.modelMapper.ToDto(this.phoneNumbersRepository.Update(userId, phoneNumberId, phoneNumber));
            }
            catch (UnauthorizedOperationException)
            {
                throw;
            }
        }

        public void Delete(int userId, int phoneNumberId, User user)
        {
            try
            {
                if (!user.Role.Name.Equals("Admin"))
                {
                    throw new UnauthorizedOperationException("You are not authorized to delete a phone number!");
                }
                this.phoneNumbersRepository.Delete(userId, phoneNumberId);
            }
            catch (UnauthorizedOperationException)
            {
                throw;
            }
        }
    }
}
