using DevEval.Application.Products.Commands;
using DevEval.Application.Products.Queries;
using DevEval.Common.Helpers.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DevEval.Application.Products.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace DevEval.API.Controllers
{
    /// <summary>
    /// API for managing Products, including retrieval, creation, updating, and deletion of products.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator instance for handling requests.</param>
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all products with pagination.
        /// </summary>
        /// <param name="parameters">Pagination parameters, including page number and page size.</param>
        /// <returns>A paginated list of products.</returns>
        /// <response code="200">Returns the paginated list of products.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<ProductDto>), 200)]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetProducts([FromQuery] PaginationParameters parameters)
        {
            var query = new GetProductsQuery(parameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
        /// <response code="200">Returns the product.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound(new { Message = $"Product with ID {id} not found." });

            return Ok(result);
        }

        /// <summary>
        /// Retrieves products by category with pagination.
        /// </summary>
        /// <param name="category">The category to filter products by.</param>
        /// <param name="parameters">Pagination parameters, including page number and page size.</param>
        /// <returns>A paginated list of products in the specified category.</returns>
        /// <response code="200">Returns the paginated list of products in the category.</response>
        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(PaginatedResult<ProductDto>), 200)]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetProductsByCategory(string category, [FromQuery] PaginationParameters parameters)
        {
            var query = new GetProductsByCategoryQuery(category, parameters);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="command">The command containing product details.</param>
        /// <returns>The created product.</returns>
        /// <response code="201">Returns the created product.</response>
        /// <response code="400">If the product data is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand command)
        {
            if (command == null)
                return BadRequest(new { Message = "Invalid product data." });

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="command">The command containing updated product details.</param>
        /// <returns>No content if the update is successful.</returns>
        /// <response code="204">If the product is updated successfully.</response>
        /// <response code="400">If the product data is invalid.</response>
        /// <response code="404">If the product is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
        {
            if (command == null)
                return BadRequest(new { Message = "Invalid product data." });

            command.Id = id; // Ensure the ID matches the route
            var result = await _mediator.Send(command);

            if (result is null)
                return NotFound(new { Message = $"Product with ID {id} not found." });

            return NoContent();
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <response code="204">If the product is deleted successfully.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Retrieves all available categories.
        /// </summary>
        /// <returns>A list of product categories.</returns>
        /// <response code="200">Returns the list of categories.</response>
        [HttpGet("categories")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            var query = new GetCategoriesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}