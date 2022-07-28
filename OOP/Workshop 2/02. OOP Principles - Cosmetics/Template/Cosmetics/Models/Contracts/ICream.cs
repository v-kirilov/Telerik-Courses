using System;
using System.Collections.Generic;
using System.Text;
using Cosmetics.Models.Enums;


namespace Cosmetics.Models.Contracts
{
    public interface ICream : IProduct
    {
        ScentType Scent { get; }
    }
}
