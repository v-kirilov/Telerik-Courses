using Cosmetics.Core.Contracts;
using Cosmetics.Helpers;
using Cosmetics.Models.Contracts;
using System.Collections.Generic;

namespace Cosmetics.Commands
{
    public class ShowCategoryCommand : BaseCommand
    {
        public const int ExpectedNumberOfArguments = 1;

        public ShowCategoryCommand(IList<string> parameters, IRepository repository)
            : base(parameters, repository)
        {
        }

        public override string Execute()
        {
            ValidationHelper.ValidateArgumentsCount(this.CommandParameters, ExpectedNumberOfArguments);

            string categoryToShow = this.CommandParameters[0];

            return ShowCategory(categoryToShow);
        }

        private string ShowCategory(string categoryName)
        {
            ICategory category = this.Repository.FindCategoryByName(categoryName);

            return category.Print();
        }
    }
}
