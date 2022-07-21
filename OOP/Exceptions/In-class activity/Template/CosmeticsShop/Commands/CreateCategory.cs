using CosmeticsShop.Core;
using CosmeticsShop.Models;
using System;
using System.Collections.Generic;

namespace CosmeticsShop.Commands
{
    public class CreateCategory : ICommand
    {
        private readonly CosmeticsRepository cosmeticsRepository;

        public CreateCategory(CosmeticsRepository productRepository)
        {
            this.cosmeticsRepository = productRepository;
        }

        public string Execute(List<string> parameters)
        {
            
            InvalidUserInputException.ValidateArgumentsCount(parameters, 1,"Category");
            string categoryName = parameters[0];

            //Implemented in CosmeticRepository class
            this.cosmeticsRepository.CreateCategory(categoryName);

            return $"Category with name {categoryName} was created!";
        }
    }
}
