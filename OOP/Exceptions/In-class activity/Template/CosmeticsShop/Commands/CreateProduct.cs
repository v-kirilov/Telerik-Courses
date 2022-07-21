using CosmeticsShop.Core;
using CosmeticsShop.Models;

using System;
using System.Collections.Generic;
using System.Globalization;

namespace CosmeticsShop.Commands
{
    public class CreateProduct : ICommand
    {
        private readonly CosmeticsRepository cosmeticsRepository;

        public CreateProduct(CosmeticsRepository productRepository)
        {
            this.cosmeticsRepository = productRepository;
        }

        public string Execute(List<string> parameters)
        {
            InvalidUserInputException.ValidateArgumentsCount(parameters, 4,"Product");
            string name = parameters[0];
            
            string brand = parameters[1];


            InvalidUserInputException.ValidatePrice(parameters[2]);
            double price = double.Parse(parameters[2], CultureInfo.InvariantCulture);
             

            InvalidUserInputException.ValidateGenderFormat(parameters[3]);
            GenderType gender = Enum.Parse<GenderType>(parameters[3], true);

            //Implemented in CosmeticRepository class
            this.cosmeticsRepository.CreateProduct(name, brand, price, gender);

            return $"Product with name {name} was created!";
        }
    }
}
