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
using BotMarketplace.API.DTOs.Products;
using BotMarketplace.API.DTOs.Users;
using BotMarketplace.Tests.Factories;
using Microsoft.AspNetCore.Mvc;

namespace BotMarketplace.Tests.BotMarketplace.API.Controllers
{
    [TestClass]
    public class ProductControllerTests : BasicControllerTests<ProductBaseDTO, ProductController>
    {
        public override Faker<ProductBaseDTO> CreateFaker()
        {
            return ProductFakeFactory.FakeProductBaseDTOMaker();
        }
        public override string UpdateDto(ref ProductBaseDTO dto)
        {
            var newName = "newName";
            dto.Name = newName;

            return newName;
        }
    }
}
