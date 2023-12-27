using BotMarketplace.API.DTOs.Transactions;
using BotMarketplace.Core.Models;
using BotMarketplace.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BotMarketplace.API.Controllers
{
    /// <summary>
    /// Handles transaction-related operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BasicController<TransactionBaseDTO, Transaction, ITransactionService>
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _service;

        /// <summary>
        /// Initializes a new instance of the TransactionController.
        /// </summary>
        /// <param name="logger">The logger for capturing logs.</param>
        /// <param name="service">The transaction service for business logic.</param>
        public TransactionController(ILogger<TransactionController> logger, ITransactionService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Creates a new transaction
        /// </summary>
        /// <param name="ProductId">The product id</param>
        /// <param name="BuyerId">The buyer id</param>
        /// <param name="SellerId">The seller id</param>
        /// <param name="Price">The transaction price</param>
        /// <returns>A newly created transaction</returns>
        /// <remarks>
        /// 
        ///     POST /api/transaction
        ///     {
        ///         "ProductId: "the-product-id"
        ///         "BuyerId": "the-user-id-of-the-buyer"
        ///         "SellerId": "the-user-id-of-the-seller"
        ///         "Price": 99.99
        ///      }
        ///      
        /// </remarks>
        /// <response code="201">Returns the newly created product</response>
        /// <response code="400">If the required values are not sent or are invalid</response>
        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] TransactionBaseDTO dto)
        {
            return await base.Create(dto);
        }

        /// <summary>
        /// Maps a TransactionBaseDTO to a Transaction model.
        /// </summary>
        /// <param name="dto">The TransactionBaseDTO containing the data for the transaction.</param>
        /// <returns>The Transaction model populated with data from the DTO.</returns>

        protected override Transaction MapToModel(TransactionBaseDTO dto)
        {
            var transaction = new Transaction(dto.ProductId, dto.BuyerId, dto.SellerId, dto.Price);

            return transaction;
        }

        /// <summary>
        /// Updates a Transaction model using data from a TransactionBaseDTO.
        /// </summary>
        /// <param name="model">The existing Transaction model to update.</param>
        /// <param name="dto">The TransactionBaseDTO containing updated data for the transaction.</param>
        protected override void UpdateModel(Transaction model, TransactionBaseDTO dto)
        {
            model.Price = dto.Price;
        }
    }
}
