using MatchScore.Models;
using MatchScore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Services.Contracts
{
    public interface ICountriesService
    {
        public List<Country> GetAll();
        Country GetById(int id);
        Country GetByName(string name);
        Country Create(Country country);
        Country Update(int id, Country country, User authUser);
        void Delete(int id, User authUser);
        bool CountryExists(string countryName);
    }
}
