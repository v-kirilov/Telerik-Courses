using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using System;
using System.Collections.Generic;
using Cosmetics.Models;
using Cosmetics.Models.Enums;

namespace Cosmetics.Commands
{
    public class CreateCreamCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 5;

        public CreateCreamCommand(IList<string> parameters, IRepository repository) 
            : base(parameters, repository)
        {

        }

        public override string Execute()
        {
            ValidationHelper.ValidateArgumentsCount(this.CommandParameters, ExpectedNumberOfArguments);
            string name = this.CommandParameters[0];
            string brand = this.CommandParameters[1];
            decimal price = ParseDecimalParameter(this.CommandParameters[2], "price");
            GenderType gender = ParseGenderType(this.CommandParameters[3]);
            ScentType scent = Enum.Parse < ScentType > (this.CommandParameters[4]);

            if (Repository.ProductExists(name))
            {
                throw new ArgumentException("Product already exists.");
            }
            Repository.CreateCream(name, brand, price, gender, scent);
            
            return $"Cream with name {name} was created!";
        }
    }
}
