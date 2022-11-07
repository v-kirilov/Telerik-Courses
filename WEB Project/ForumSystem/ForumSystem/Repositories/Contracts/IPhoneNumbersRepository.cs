using ForumSystem.Models;
using System.Collections.Generic;

namespace ForumSystem.Repositories.Contracts
{
    public interface IPhoneNumbersRepository
    {
        List<PhoneNumber> GetAll();
        PhoneNumber GetByUserId(int userId);
        PhoneNumber GetById(int userId, int phoneNumberId);
        PhoneNumber Create(PhoneNumber phoneNumber);
        PhoneNumber Update(int userId, int phoneNumberId, PhoneNumber phoneNumber);
        void Delete(int userId, int phoneNumberId);

    }
}
