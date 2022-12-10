using MatchScore.Data;
using MatchScore.Exceptions;
using MatchScore.Models;
using MatchScore.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Repositories
{
    public class SportClubsRepository : ISportClubsRepository
    {
        private const string InvalidNameErrorMessage = "This sport club name does not exist";
        private readonly ApplicationContext context;

        public SportClubsRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public List<SportClub> GetAll()
        {
            var sportClubs = this.GetSportClubs().ToList();
            return sportClubs;
        }

        public SportClub GetById(int id)
        {
            var sportClub = this.GetSportClubs().FirstOrDefault(r => r.Id == id);
            return sportClub ?? throw new EntityNotFoundException();
        }

        public SportClub GetByName(string name)
        {
            var sportClub = this.GetSportClubs().FirstOrDefault(r => r.Name == name);
            return sportClub ?? throw new EntityNotFoundException(InvalidNameErrorMessage);
        }

        public SportClub Create(SportClub sportClub)
        {
            this.context.SportClubs.Add(sportClub);
            this.context.SaveChanges();

            return sportClub;
        }

        public SportClub UpdateName(int id, string sportClubName)
        {
            var sportClubToUpdate = this.GetById(id);
            sportClubToUpdate.Name = sportClubName;
            this.context.Update(sportClubToUpdate);
            this.context.SaveChanges();

            return sportClubToUpdate;
        }

        public void Delete(int id)
        {
            var existingSportClub = this.GetById(id);
            existingSportClub.IsDeleted = true;

            this.context.Update(existingSportClub);
            this.context.SaveChanges();
        }

        private IQueryable<SportClub> GetSportClubs()
        {
            return this.context.SportClubs.Where(c => c.IsDeleted == false)
                    .Include(c => c.Players);
        }
    }
}
