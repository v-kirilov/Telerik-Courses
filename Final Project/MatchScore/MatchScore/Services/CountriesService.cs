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
    public class CountriesService : ICountriesService
    {
        private const string DuplicateNameErrorMessage = "This country already exists.";
        private const string ModifyNameErrorMessage = "Only the admin can update this country's name.";
        private const string DeleteCountryErrorMessage = "Only the admin can delete country.";

        private readonly ICountriesRepository countriesRepository;
        private readonly IModelMapper modelMapper;

        public CountriesService(ICountriesRepository countriesRepository, IModelMapper modelMapper)
        {
            this.countriesRepository = countriesRepository;
            this.modelMapper = modelMapper;
        }

        public List<Country> GetAll()
        {
            var countries = this.countriesRepository.GetAll();
            return countries;
        }

        public Country GetById(int id)
        {
            var country = this.countriesRepository.GetById(id);
            return country;
        }

        public Country GetByName(string name)
        {
            var country = this.countriesRepository.GetByName(name);
            return country;
        }

        public Country Create(Country country)
        {
            //var country = this.modelMapper.MapDtoToCountry(countryDto);
            bool duplicateExists = CountryExists(country.Name);

            if (duplicateExists)
            {
                throw new DuplicateEntityException(DuplicateNameErrorMessage);
            }

            var createdCountry= this.countriesRepository.Create(country);

            return createdCountry;
        }

        public Country Update(int id, Country countryIncoming, User authUser)
        {
            var countryToUpdate = this.GetById(id);
            //var countryIncoming = this.modelMapper.MapDtoToCountry(countryDto);

            if (authUser.Role.Name != "Admin")
            {
                throw new UnauthorizedOperationException(ModifyNameErrorMessage);
            }

            bool duplicateExists = CountryExists(countryIncoming.Name);

            if (duplicateExists)
            {
                throw new DuplicateEntityException(DuplicateNameErrorMessage);
            }

            var updatedCountry = this.countriesRepository.UpdateName(id, countryIncoming.Name);

            return updatedCountry;
        }

        public void Delete(int id, User authUser)
        {
            if (authUser.Role.Name != "Admin")
            {
                throw new UnauthorizedOperationException(DeleteCountryErrorMessage);
            }

            this.countriesRepository.Delete(id);
        }

        public bool CountryExists(string countryName)
        {
            bool duplicateExists = true;

            try
            {
                this.countriesRepository.GetByName(countryName);
            }
            catch (EntityNotFoundException)
            {
                duplicateExists = false;
            }

            return duplicateExists;
        }
    }
}
