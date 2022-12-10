using MatchScore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Repositories.Contracts
{
    public interface ICountriesRepository
    {
        List<Country> GetAll();
        Country GetById(int id);
        Country GetByName(string name);
        Country Create(Country country);
        Country UpdateName(int id, string countryName);
        void Delete(int id);
    }
}
