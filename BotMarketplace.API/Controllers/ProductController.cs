using BotMarketplace.API.DTOs.Products;
using BotMarketplace.Core.Models;
using BotMarketplace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotMarketplace.API.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductController : BasicController<ProductBaseDTO, Product, IProductService>
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _service;

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
        ///     POST /api/products
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

        protected override void UpdateModel(Product model, ProductBaseDTO dto)
        {
            model.Name = dto.Name;
            model.Description = dto.Description;
            model.Price = dto.Price;
        }
    }
}
