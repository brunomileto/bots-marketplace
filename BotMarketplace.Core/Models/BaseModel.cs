using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Core.Interfaces;

namespace BotMarketplace.Core.Models
{
    public class BaseModel : IBaseModel
    {
        public string Id { get; private set; }
        public DateTime DateCreated { get; private set; }

        public BaseModel() 
        { 
            Id = Guid.NewGuid().ToString();
            DateCreated = DateTime.UtcNow;
        }

        public BaseModel(string id, DateTime dateCreated)
        {
            Id = id;
            DateCreated = dateCreated;
        }
    }
}
