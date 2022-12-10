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
    public class CountriesRepository : ICountriesRepository
    {
        private const string InvalidNameErrorMessage = "This country name does not exist";
        private readonly ApplicationContext context;

        public CountriesRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public List<Country> GetAll()
        {
            var countries = this.GetCountries().ToList();
            return countries;
        }

        public Country GetById(int id)
        {
            var country = this.GetCountries().FirstOrDefault(r => r.Id == id);
            return country ?? throw new EntityNotFoundException();
        }

        public Country GetByName(string name)
        {
            var country = this.GetCountries().FirstOrDefault(r => r.Name == name);
            return country ?? throw new EntityNotFoundException(InvalidNameErrorMessage);
        }

        public Country Create(Country country)
        {
            this.context.Countries.Add(country);
            this.context.SaveChanges();

            return country;
        }

        public Country UpdateName(int id, string countryName)
        {
            var countryToUpdate = this.GetById(id);
            countryToUpdate.Name = countryName;
            this.context.Update(countryToUpdate);
            this.context.SaveChanges();

            return countryToUpdate;
        }

        public void Delete(int id)
        {
            var existingCountry = this.GetById(id);
            existingCountry.IsDeleted = true;

            this.context.Update(existingCountry);
            this.context.SaveChanges();
        }

        private IQueryable<Country> GetCountries()
        {
            return this.context.Countries.Where(c => c.IsDeleted == false)
                    .Include(c => c.Players);
        }
    }
}
