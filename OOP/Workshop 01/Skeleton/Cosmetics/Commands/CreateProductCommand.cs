using System;
using System.Collections.Generic;
using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using Cosmetics.Models;

namespace Cosmetics.Commands
{
    public class CreateProductCommand : ICommand
    {
        private const string ProductCreated = "Product with name {0} was created!";
        private const string ProductAlreadyExists = "Product with name {0} already exists!";
        private const string InvalidPrice = "Invalid value for price. Should be a number.";
        private const string NoSuchEnum = "None of the enums in GenderType matches the value {0}.";

        public const int ExpectedNumberOfArguments = 4;

        private readonly List<string> commandParameters;
        private readonly IRepository repository;

        public CreateProductCommand(List<string> commandParameters, IRepository repository)
        {
            this.commandParameters = commandParameters;
            this.repository = repository;
        }

        public string Execute()
        {
            ValidationHelpers.ValidateArgumentsCount(commandParameters, ExpectedNumberOfArguments);
            string name = commandParameters[0];
            string brand = commandParameters[1];
            double price = ParsingHelpers.TryParseDouble(commandParameters[2], InvalidPrice);
            GenderType gender = ParsingHelpers.TryParseGender(commandParameters[3], NoSuchEnum);
            return CreateProduct(name, brand, price, gender);
        }

        private string CreateProduct(string name, string brand, double price, GenderType gender)
        {
            if (repository.ProductExist(name))
            {
                throw new ArgumentException(string.Format(ProductAlreadyExists, name));
            }

            repository.CreateProduct(name, brand, price, gender);

            return string.Format(ProductCreated, name);
        }
    }
}
