using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.API;
using BotMarketplace.API.DTOs;
using BotMarketplace.Common.Extensions;
using BotMarketplace.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BotMarketplace.Tests.BotMarketplace.API.Controllers
{
    [TestClass]
    public abstract class BasicControllerTests<TDto, TController> 
        where TDto : BaseDTO
        where TController : ControllerBase
    {
        protected readonly HttpClient _httpClient = default!;
        private Faker<TDto> _faker = default!;
        protected TDto _dto = default!;
        private string _dtoId = default!;
        private List<PropertyInfo> _dtoProperties = default!;
        private readonly GetPaginatedParamsDelegate _getPaginatedParams = default!;

        protected string GetControllerRoute()
        {
            var controllerName = typeof(TController).Name.Replace("Controller", "");
            var routeAttribute = typeof(TController).GetCustomAttributes(typeof(RouteAttribute), true)
                .FirstOrDefault() as RouteAttribute;

            return routeAttribute?.Template.Replace("[controller]", controllerName.ToLowerInvariant()) ?? string.Empty;
        }

        protected bool ItemExistsOnList(List<TDto> itemList, TDto itemToCheck)
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

        protected abstract string UpdateDto(ref TDto dto);

        public BasicControllerTests()
        {
            var factory = new CustomWebApplicationFactory<Program>();

            _getPaginatedParams = new GetPaginatedParamsDelegate(factory.GetPaginatedParams);
            _httpClient = factory.CreateClient();
            _faker = CreateFaker();
            _dto = _faker.Generate();
            _dtoProperties = typeof(TDto).GetProperties().ToList();
        }

        protected abstract Faker<TDto> CreateFaker();

        protected async Task<TDto> CreateDtoOnDb()
        {
            var createdDto = await (await _httpClient.PostAsJsonAsync(GetControllerRoute(), _dto)).Content.ReadFromJsonAsync<TDto>();

            return createdDto!;
        }

        protected async Task<TDto?> GetDtoFromDb(string id)
        {
            var dto = await (await _httpClient.GetAsync($"{GetControllerRoute()}/{id}")).Content.ReadFromJsonAsync<TDto>();
            if (dto == null || dto.Id == null)
                return null;
            return dto;
        }

        [TestMethod]
        protected virtual async Task CreateDto_ShouldReturnCreated()
        {
            var newDto = _faker.Generate();

            var response = await _httpClient.PostAsJsonAsync(GetControllerRoute(), newDto);
            var json = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            
            var createdDto = await response.Content.ReadFromJsonAsync<TDto>();

            Assert.IsNotNull(createdDto);
            Assert.IsTrue(!string.IsNullOrEmpty(createdDto.Id));

        }

        [TestMethod]
        protected virtual async Task GetAllDto_ShouldReturnOk()
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

        [TestMethod]
        protected virtual async Task GetById_ShouldReturnItem()
        {
            var createdDto = await CreateDtoOnDb();

            var response = await _httpClient.GetAsync($"{GetControllerRoute()}/{createdDto.Id}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var userReturned = await response.Content.ReadFromJsonAsync<TDto>();

            Assert.IsNotNull(userReturned);

            foreach (var prop in _dtoProperties)
                Assert.AreEqual(prop.GetValue(createdDto, null), prop.GetValue(createdDto, null));

        }

        [TestMethod]
        protected virtual async Task UpdateUser_ShouldReturnNoContent()
        {
            var dtoToUpdate = await CreateDtoOnDb();

            var valueUpdated = UpdateDto(ref dtoToUpdate);

            var response = await _httpClient.PutAsJsonAsync($"{GetControllerRoute()}/{dtoToUpdate.Id}", dtoToUpdate);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            Assert.IsNotNull(dtoToUpdate.Id);

            var updatedDto = await GetDtoFromDb(dtoToUpdate.Id);
            Assert.IsNotNull(updatedDto);
            foreach (var prop in _dtoProperties)
            {
                Assert.AreEqual(prop.GetValue(dtoToUpdate, null), prop.GetValue(updatedDto, null));
            }
        }

        [TestMethod]
        protected virtual async Task DeleteUser_ShouldReturnNoContent()
        {
            var createdDto = await CreateDtoOnDb();

            var response = await _httpClient.DeleteAsync($"{GetControllerRoute()}/{createdDto.Id}");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            var deletedDto = await GetDtoFromDb(createdDto.Id!);
            Assert.IsNull(deletedDto);
        }
    }
}
