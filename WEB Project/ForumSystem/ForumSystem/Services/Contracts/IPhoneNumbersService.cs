using ForumSystem.Models;
using ForumSystem.Models.DTO;
using System.Collections.Generic;

namespace ForumSystem.Services.Contracts
{
    public interface IPhoneNumbersService
    {
        List<PhoneNumberDto> GetAll();
        PhoneNumberDto GetByUserId(int userId);
        PhoneNumberDto GetById(int userId, int phoneNumberId);
        PhoneNumberDto Create(PhoneNumber phoneNumber, User user);
        PhoneNumberDto Update(int userId, int phoneNumberId, PhoneNumber phoneNumber, User user);
        void Delete(int userId, int phoneNumberId, User user);

    }
}
