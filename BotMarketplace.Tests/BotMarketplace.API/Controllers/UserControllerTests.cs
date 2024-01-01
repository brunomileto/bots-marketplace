using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.API;
using BotMarketplace.API.Controllers;
using BotMarketplace.API.DTOs.Users;
using BotMarketplace.Core.Models;
using BotMarketplace.Tests.Factories;
using Microsoft.AspNetCore.Mvc;

namespace BotMarketplace.Tests.BotMarketplace.API.Controllers
{
    [TestClass]
    public class UserControllerTests : BasicControllerTests<UserBaseDTO, UserController>
    {
        public override Faker<UserBaseDTO> CreateFaker ()
        {
            return UserFakeFactory.FakeUserBaseDTOMaker();
        }

        public override string UpdateDto(ref UserBaseDTO dto)
        {
            var newName = "newName";
            dto.Name = newName;

            return newName;
        }
    }
}
