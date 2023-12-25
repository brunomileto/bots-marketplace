using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotMarketplace.Core.Interfaces
{
    public interface IBaseModel
    {
        string Id { get; }
        DateTime DateCreated { get; }
    }
}
