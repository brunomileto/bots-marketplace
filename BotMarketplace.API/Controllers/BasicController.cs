using BotMarketplace.Core.Interfaces;
using BotMarketplace.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotMarketplace.API.Controllers
{
    public abstract class BasicController<TModelDTO, TModel, TService> : ControllerBase
        where TModelDTO : class
        where TModel : class, IBaseModel
        where TService : IBaseService<TModel>
    {
        protected readonly TService _service;

        public BasicController(TService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all itens paginated
        /// </summary>
        /// <param name="pageNumber">Number of the page</param>
        /// <param name="perPage">Quantity of itens returned</param>
        /// <returns>Paginated information and the itens found</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/products
        ///     {
        ///        "pageNumber": 1,
        ///        "perPage": 5,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the paginated information and itens found, if any</response>
        [HttpGet]
        public virtual async Task<IActionResult> GetAll(int pageNumber = 1, int perPage = 5)
        {
            if (pageNumber < 1 || perPage < 1)
                return BadRequest("Invalid page number or per page");

            var response = await _service.GetPaginationAsync(pageNumber, perPage);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve</param>
        /// <returns>The requested item if found</returns>
        /// <response code="200">Returns the requested item</response>
        /// <response code="404">If the item with the specified ID is not found</response>

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(string id)
        {
            var response = await _service.GetByIdAsync(id);

            if (response == null)
                return NotFound();
            
            return Ok(response);
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="dto">The data transfer object containing the item's data</param>
        /// <returns>A newly created item</returns>
        /// <remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null or invalid</response>
        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TModelDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = MapToModel(dto);

            await _service.CreateAsync(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        /// <param name="id">The ID of the item to update</param>
        /// <param name="dto">The data transfer object containing the updated data</param>
        /// <returns>No content</returns>
        /// <response code="204">If the item was updated successfully</response>
        /// <response code="400">If the item is null or invalid</response>
        /// <response code="404">If the item with the specified ID is not found</response>

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(string id, [FromBody] TModelDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var modelData = await _service.GetByIdAsync(id);
            
            if (modelData == null)
                return NotFound();

            UpdateModel(modelData, dto);

            await _service.UpdateAsync(modelData);

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">If the item was deleted successfully</response>
        /// <response code="404">If the item with the specified ID is not found</response>

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model == null)
                return NotFound();

            await _service.DeleteAsync(id);

            return NoContent();
        }

        protected abstract TModel MapToModel(TModelDTO dto);
        protected abstract void UpdateModel(TModel model, TModelDTO dto);
    }
}
