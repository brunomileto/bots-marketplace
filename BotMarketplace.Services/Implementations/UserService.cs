using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotMarketplace.Core.Models;
using BotMarketplace.Infrastructure.Repositories.Interfaces;
using BotMarketplace.Services.Interfaces;

namespace BotMarketplace.Services.Implementations
{
    public class UserService : BaseService<User, IUserRepository>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository) 
        { 
        }

    }
}
