using CosmeticsShop.Commands;
using CosmeticsShop.Models;
using System;

namespace CosmeticsShop.Core
{
    public class CommandFactory
    {
        public ICommand CreateCommand(string commandTypeValue, CosmeticsRepository productRepository)
        {
            CommandType commandType = Enum.Parse<CommandType>(commandTypeValue, true);
            InvalidUserInputException.ValidateCommandFormat(commandTypeValue);

            switch (commandType)
            {
                case CommandType.CreateCategory:
                    return new CreateCategory(productRepository);
                case CommandType.CreateProduct:
                    return new CreateProduct(productRepository);
                case CommandType.AddProductToCategory:
                    return new AddProductToCategory(productRepository);
                case CommandType.ShowCategory:
                    return new ShowCategory(productRepository);
                default:
                    throw new ArgumentException($"Command {commandTypeValue} is not supported.");
                    
                    //still kinda sketchy
            }
        }
    }
}
