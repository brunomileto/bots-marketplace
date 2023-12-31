using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.API;
using BotMarketplace.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BotMarketplace.Tests.BotMarketplace.API.Controllers
{
    [TestClass]
    public abstract class BasicControllerTests<TDto, TController> 
        where TDto : class
        where TController : ControllerBase
    {
        private readonly HttpClient _httpClient = default!;
        private Faker<TDto> _faker = default!;
        private TDto _dto = default!;
        private List<PropertyInfo> _dtoProperties = default!;
        private readonly GetPaginatedParamsDelegate _getPaginatedParams = default!;

        public string GetControllerRoute()
        {
            var controllerName = typeof(TController).Name.Replace("Controller", "");
            var routeAttribute = typeof(TController).GetCustomAttributes(typeof(RouteAttribute), true)
                .FirstOrDefault() as RouteAttribute;

            return routeAttribute?.Template.Replace("[controller]", controllerName.ToLowerInvariant()) ?? string.Empty;
        }

        public BasicControllerTests()
        {
            var factory = new CustomWebApplicationFactory<Program>();

            _getPaginatedParams = new GetPaginatedParamsDelegate(factory.GetPaginatedParams);
            _httpClient = factory.CreateClient();
            _faker = CreateFaker();
            _dto = _faker.Generate();
            _dtoProperties = typeof(TDto).GetProperties().ToList();
        }

        public abstract Faker<TDto> CreateFaker();

        [TestMethod]
        public async Task CreateDto_ShouldReturnCreated()
        {
            var newDto = _faker.Generate();

            var response = await _httpClient.PostAsJsonAsync(GetControllerRoute(), newDto);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            
            var createdDto = await response.Content.ReadFromJsonAsync<TDto>();

            Assert.IsNotNull(createdDto);

            foreach (var prop in _dtoProperties)
                Assert.AreEqual(prop.GetValue(createdDto, null), prop.GetValue(newDto, null));
        }

        [TestMethod]
        public async Task GetAllDto_ShouldReturnOk()
        {
            var pageNumber = 1;
            var perPage = 2;

            var createdDto = await (await _httpClient.PostAsJsonAsync(GetControllerRoute(), _dto)).Content.ReadFromJsonAsync<TDto>();

            var queryString = _getPaginatedParams(pageNumber, perPage);
            var response = await _httpClient.GetAsync($"{GetControllerRoute()}?{queryString}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(createdDto);

            var paginatedContent = await response.Content.ReadFromJsonAsync<PaginationResponse<TDto>>();
            Assert.IsNotNull(paginatedContent);
            Assert.IsNotNull(paginatedContent.Items);
            Assert.IsTrue(paginatedContent.Items.Count() > 0);

            Assert.IsTrue(ItemExistsOnList(paginatedContent.Items.ToList(), createdDto!));
        }

        public bool ItemExistsOnList(List<TDto> itemList, TDto itemToCheck)
        {
            var exists = false;
            foreach (var item in itemList)
            {
                foreach (var prop in _dtoProperties)
                {
                    var propValueItem = prop.GetValue(item, null);
                    var propValueItemToCheck = prop.GetValue(item, null);

                    if (propValueItem == null && propValueItemToCheck == null)
                        exists = true;
                    else if (propValueItem != null && propValueItemToCheck != null)
                        exists = propValueItem.Equals(propValueItemToCheck);

                    if (!exists)
                        break;
                }

                if (exists)
                    return true;
            }

            return exists;
        }
    }
}
