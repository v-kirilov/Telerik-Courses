using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using Cosmetics.Models.Enums;
using System;
using System.Collections.Generic;
using Cosmetics.Models;


namespace Cosmetics.Commands
{
    public class CreateShampooCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 6;

        public CreateShampooCommand(IList<string> parameters, IRepository repository)
            : base(parameters, repository)
        {

        }

        public override string Execute()
        {
            ValidationHelper.ValidateArgumentsCount(this.CommandParameters, ExpectedNumberOfArguments);
            string name = this.CommandParameters[0];
            string brand = this.CommandParameters[1];
            decimal price = decimal.Parse(this.CommandParameters[2]);
            GenderType gender = ParseGenderType(this.CommandParameters[3]);
            int millilitres = ParseIntParameter(this.CommandParameters[4], "Millilitres");
            UsageType usage = Enum.Parse<UsageType>(this.CommandParameters[5]);


            
            if (Repository.ProductExists(name))
            {
                throw new ArgumentException("Product already exists.");
            }
            Repository.CreateShampoo(name, brand, price, gender, millilitres, usage);
            return $"Shampoo with name {name} was created!";

            //throw new NotImplementedException("Not implemented yet.");
        }

    }
}
