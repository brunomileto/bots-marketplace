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
        //private readonly HttpClient _client;
        //private Faker<UserBaseDTO> _faker;
        //private UserBaseDTO _user;
        //private readonly GetPaginatedParamsDelegate _getPaginatedParams;
        
        //public string GetControllerRoute<TController>() where TController : ControllerBase
        //{
        //    var controllerName = typeof(TController).Name.Replace("Controller", "");
        //    var routeAttribute = typeof(TController).GetCustomAttributes(typeof(RouteAttribute), true)
        //                                            .FirstOrDefault() as RouteAttribute;

        //    return routeAttribute?.Template.Replace("[controller]", controllerName.ToLowerInvariant()) ?? string.Empty;
        //}

        //public UserControllerTests()
        //{
        //    var factory = new CustomWebApplicationFactory<Program>();
            
        //    GetPaginatedParamsDelegate getPaginatedParamsDelegate = new GetPaginatedParamsDelegate(factory.GetPaginatedParams);
        //    _getPaginatedParams = getPaginatedParamsDelegate;
            
        //    _client = factory.CreateClient();
            
        //    _faker = UserFakeFactory.FakeUserBaseDTOMaker();
        //    _user = _faker.Generate();
        //}
        //[TestMethod]
        //public async Task CreateUser_ShouldReteurnCreated()
        //{
        //    var newUser = _faker.Generate();

        //    var response = await _client.PostAsJsonAsync(GetControllerRoute<UserController>(), newUser);

        //    Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        //    var createdUser = await response.Content.ReadFromJsonAsync<UserBaseDTO>();
        //    Assert.IsNotNull(createdUser);
        //    Assert.AreEqual(newUser.Name, createdUser.Name);
        //}

        //[TestMethod]
        //public async Task GetAllUsers_ShouldReturnOk()
        //{
        //    var pageNumber = 1;
        //    var perPage = 10;
        //    var queryString = _getPaginatedParams(pageNumber, perPage);
        //    var testResponse = await _client.PostAsJsonAsync(GetControllerRoute<UserController>(), _user);
        //    var userBaseDTO = await testResponse.Content.ReadFromJsonAsync<UserBaseDTO>();
        //    //var response = await _client.GetAsync($"/api/user?{queryString}");
        //    var response = await _client.GetAsync($"{GetControllerRoute<UserController>()}?{queryString}");

        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        //    var usersPaginated = await response.Content.ReadFromJsonAsync<PaginationResponse<UserBaseDTO>>();
        //    var user = usersPaginated!.Items.ToList()[0];
        //    Assert.IsNotNull(usersPaginated);
        //    Assert.IsNotNull(usersPaginated.Items);
        //    Assert.IsNotNull(usersPaginated.Items.Where(u => u.Name == userBaseDTO!.Name && u.Email == userBaseDTO!.Email));
        //}
    }
}
