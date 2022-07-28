using Cosmetics.Commands.Contracts;
using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmetics.Commands
{
    public class CreateCategoryCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 1;

        public CreateCategoryCommand(IList<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            ValidationHelper.ValidateArgumentsCount(this.CommandParameters, ExpectedNumberOfArguments);

            string categoryName = this.CommandParameters[0];

            return CreateCategory(categoryName);
        }

        private string CreateCategory(string categoryName)
        {
            if (this.Repository.CategoryExists(categoryName))
            {
                throw new ArgumentException(string.Format($"Category with name {categoryName} already exists!"));
            }

            this.Repository.CreateCategory(categoryName);

            return $"Category with name {categoryName} was created!";
        }
    }
}
