using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using System;
using System.Collections.Generic;
using Cosmetics.Models;
using Cosmetics.Models.Enums;



namespace Cosmetics.Commands
{
    public class CreateToothpasteCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 5;

        public CreateToothpasteCommand(IList<string> parameters, IRepository repository)
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
            string ingredients = this.CommandParameters[4];

            if (Repository.ProductExists(name))
            {
                throw new ArgumentException("Product already exists.");
            }
            Repository.CreateToothpaste(name, brand, price, gender, ingredients);
            //Toothpaste toothPaste = new Toothpaste(name, brand, price, gender, ingredients);
            // Repository.Products.Add(toothPaste);
            //throw new NotImplementedException("Not implemented yet.");
            return $"Toothpaste with name {name} was created!";
        }

    }
}
