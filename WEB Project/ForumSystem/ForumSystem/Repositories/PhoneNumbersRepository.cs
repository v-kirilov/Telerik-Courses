using ForumSystem.Data;
using ForumSystem.Exceptions;
using ForumSystem.Models;
using ForumSystem.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ForumSystem.Repositories
{
    public class PhoneNumbersRepository : IPhoneNumbersRepository
    {
        private readonly ApplicationContext context;

        public PhoneNumbersRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public List<PhoneNumber> GetAll()
        {
            return this.GetPhoneNumbers().ToList();
        }


        public PhoneNumber GetByUserId(int userId)
        {
            PhoneNumber phoneNumber = this.GetAll().Where(b => b.UserId == userId).FirstOrDefault();
            
            return phoneNumber ?? throw new EntityNotFoundException($"The user with id: {userId} has not a phone number.");
        }
        public PhoneNumber GetById(int userId, int phoneNumberId)
        {
            PhoneNumber phoneNumber = this.GetByUserId(userId);

            if (phoneNumber.Id == phoneNumberId)
            {
                return phoneNumber;
            }
            else
            {
                throw new EntityNotFoundException($"The user with id: {userId} has not a phone number with id: {phoneNumberId}.");
            }
        }

        public PhoneNumber Create(PhoneNumber phoneNumber)
        {
            //phoneNumber.Id = this.context.PhoneNumbers.Count();
            this.context.PhoneNumbers.Add(phoneNumber);
            this.context.SaveChanges();

            return phoneNumber;
        }

        public PhoneNumber Update(int userId, int phoneNumberId, PhoneNumber phoneNumber)
        {
            PhoneNumber phoneNumberToUpdate = this.GetById(userId, phoneNumberId);
            phoneNumberToUpdate.Number = phoneNumber.Number;
            this.context.Update(phoneNumberToUpdate);
            this.context.SaveChanges();

            return phoneNumberToUpdate;
        }

        public void Delete(int userId, int phoneNumberId)
        {
            PhoneNumber phoneNumberToDelete = this.GetById(userId, phoneNumberId);
            this.context.PhoneNumbers.Remove(phoneNumberToDelete);
            this.context.SaveChanges();
        }

        private IQueryable<PhoneNumber> GetPhoneNumbers()
        {
            return this.context.PhoneNumbers
                .Include(number => number.User);
        }
    }
}
