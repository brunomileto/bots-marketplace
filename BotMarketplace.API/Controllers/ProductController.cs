using BotMarketplace.API.DTOs.Products;
using BotMarketplace.Core.Models;
using BotMarketplace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotMarketplace.API.Controllers
{
    /// <summary>
    /// Handles product-related operations
    /// </summary>
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ProductController : BasicController<ProductBaseDTO, Product, IProductService>
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;

        /// <summary>
        /// Initializes a new instance of the ProductController.
        /// </summary>
        /// <param name="logger">The logger for capturing logs.</param>
        /// <param name="service">The product service for business logic.</param>
        public ProductController(ILogger<ProductController> logger, IProductService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="Name">The produt name</param>
        /// <param name="Description">The product description</param>
        /// <param name="Price">The product Price</param>
        /// <param name="CreatorId">The ID of the product creator</param>
        /// <returns>A newly created product</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/product
        ///     {
        ///         "Name": "Sample Product",
        ///         "Description": "Product Description",
        ///         "Price": 99.99,
        ///         "CreatorId": "the-creator-id"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created product</response>
        /// <response code="400">If the product is null or invalid</response>
        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] ProductBaseDTO dto)
        {
            return await base.Create(dto);
        }

        /// <summary>
        /// Maps a ProductBaseDTO to a Product model.
        /// </summary>
        /// <param name="dto">The ProductBaseDTO containing the data for the transaction.</param>
        /// <returns>The Product model populated with data from the DTO.</returns>
        protected override Product MapToModel(ProductBaseDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                CreatorId = dto.CreatorId,
                Description = dto.Description,
                Price = dto.Price,
            };

            return product;
        }

        /// <summary>
        /// Updates a Product model using data from a ProductBaseDTO.
        /// </summary>
        /// <param name="model">The existing Product model to update.</param>
        /// <param name="dto">The ProductBaseDTO containing updated data for the transaction.</param>
        protected override void UpdateModel(Product model, ProductBaseDTO dto)
        {
            model.Name = dto.Name;
            model.Description = dto.Description;
            model.Price = dto.Price;
        }
    }
}
