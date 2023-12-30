using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.Core.Enums;
using BotMarketplace.Core.Models;
using BotMarketplace.Infrastructure.Repositories.Implementations;
using BotMarketplace.Infrastructure.Repositories.Interfaces;
using BotMarketplace.Services.Implementations;
using Moq;

namespace BotMarketplace.Tests.BotMarketplace.Services
{

    [TestClass]
    public class BaseServicesTests
    {
        private class TestService : BaseService<BaseModel, IBaseRepository<BaseModel>>
        {
            public TestService(IBaseRepository<BaseModel> repository) : base(repository) { }
        }

        [TestMethod]
        public async Task GetPaginationAsync_ShouldReturnPaginationsItens()
        {
            var mockRepository = new Mock<IBaseRepository<BaseModel>>();
            var service = new TestService(mockRepository.Object);

            var fakeData = new Faker<BaseModel>().RuleFor(b => b.Id, f => f.Random.Uuid().ToString()).Generate(5);

            var pageNumber = 1;
            var pageSize = 2;
            var totalRecords = fakeData.Count;

            mockRepository.Setup(repo => repo.GetAllAsync(pageNumber, pageSize)).ReturnsAsync((fakeData.OrderBy(p => p.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(), totalRecords));

            var result = await service.GetPaginationAsync(pageNumber, pageSize);

            Assert.AreEqual(pageSize, result.Items.Count());
            Assert.AreEqual(pageNumber, result.CurrentPage);
            Assert.AreEqual(pageSize, result.PerPage);
            Assert.AreEqual(totalRecords, result.TotalCount);
            mockRepository.Verify(repo => repo.GetAllAsync(pageNumber, pageSize), Times.Once());
        }
    }
}
