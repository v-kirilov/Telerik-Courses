using System.Collections.Generic;

namespace CosmeticsShop.Commands
{
    public interface ICommand
    {
        public string Execute(List<string> parameters);
    }
}
