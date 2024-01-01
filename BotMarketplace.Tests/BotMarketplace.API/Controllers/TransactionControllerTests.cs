using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using BotMarketplace.API.Controllers;
using BotMarketplace.API.DTOs.Transactions;
using BotMarketplace.Tests.Factories;
using BotMarketplace.Core.Enums;

namespace BotMarketplace.Tests.BotMarketplace.API.Controllers
{
    [TestClass]
    public class TransactionControllerTests: BasicControllerTests<TransactionBaseDTO, TransactionController>
    {
        protected override Faker<TransactionBaseDTO> CreateFaker()
        {
            return TransactionFakeFactory.FakeTransactionBaseDTOMaker();
        }

        protected override string UpdateDto(ref TransactionBaseDTO dto)
        {
            decimal price = 2.99M;

            dto.Price = price;

            return price.ToString();
        }

        [TestMethod]
        protected override async Task DeleteUser_ShouldReturnNoContent()
        {
            var createdTransaction = await CreateDtoOnDb();

            var response = await _httpClient.DeleteAsync($"{GetControllerRoute()}/{createdTransaction.Id}");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            var deletedTransaction = await GetDtoFromDb(createdTransaction.Id!);

            Assert.IsNotNull(deletedTransaction);
            Assert.IsTrue(EnumTransactionStatus.Deleted.Equals(deletedTransaction.Status));
        }
    }
}
