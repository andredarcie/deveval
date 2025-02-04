using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevEval.Application.Carts.Commands;
using DevEval.Common.Helpers.Pagination;
using DevEval.Application.Carts.Queries;
using DevEval.Application.Carts.Dtos;
using DevEval.Application.Sales.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace DevEval.WebApi.Controllers
{
    /// <summary>
    /// API for managing Carts, including retrieval, creation, updating, deletion, and checkout.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartsController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator instance for handling requests.</param>
        public CartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a list of all carts with optional pagination and sorting.
        /// </summary>
        /// <param name="_page">The page number to retrieve (default: 1).</param>
        /// <param name="_size">The number of records per page (default: 10).</param>
        /// <param name="_order">Sorting criteria (e.g., "date asc, total desc").</param>
        /// <returns>A paginated list of carts.</returns>
        /// <response code="200">Returns the paginated list of carts.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<CartDto>), 200)]
        public async Task<ActionResult<PaginatedResult<CartDto>>> GetCarts(
            [FromQuery] int _page = 1,
            [FromQuery] int _size = 10,
            [FromQuery] string _order = "")
        {
            var query = new GetCartsQuery(new PaginationParameters
            {
                Page = _page,
                PageSize = _size,
                OrderBy = _order
            });

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific cart by ID.
        /// </summary>
        /// <param name="id">The ID of the cart to retrieve.</param>
        /// <returns>The cart with the specified ID.</returns>
        /// <response code="200">Returns the cart.</response>
        /// <response code="404">If the cart is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CartDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CartDto>> GetCartById(int id)
        {
            var query = new GetCartByIdQuery(id);
            var result = await _mediator.Send(query);
            return result is not null ? Ok(result) : NotFound(new { Message = "Cart not found" });
        }

        /// <summary>
        /// Adds a new cart for the authenticated user.
        /// </summary>
        /// <param name="command">The cart creation command.</param>
        /// <returns>The created cart with its details.</returns>
        /// <response code="201">Returns the created cart.</response>
        /// <response code="401">If the user is not authenticated or the UserId claim is missing.</response>
        /// <response code="400">If the UserId claim is in an invalid format.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CartDto), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CartDto>> CreateCart([FromBody] CreateCartCommand command)
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "UserId claim is missing in the token." });
            }

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                return BadRequest(new { Message = "Invalid UserId format in the token." });
            }

            command.UserId = userId;

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetCartById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing cart by ID.
        /// </summary>
        /// <param name="id">The ID of the cart to update.</param>
        /// <param name="command">The cart update command.</param>
        /// <returns>The updated cart details.</returns>
        /// <response code="200">Returns the updated cart.</response>
        /// <response code="400">If the ID in the path does not match the ID in the command.</response>
        /// <response code="404">If the cart is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CartDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CartDto>> UpdateCart(int id, [FromBody] UpdateCartCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { Message = "ID in path and body do not match" });

            var result = await _mediator.Send(command);
            return result is not null ? Ok(result) : NotFound(new { Message = "Cart not found" });
        }

        /// <summary>
        /// Deletes a cart by ID.
        /// </summary>
        /// <param name="id">The ID of the cart to delete.</param>
        /// <response code="200">If the cart is deleted successfully.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> DeleteCart(int id)
        {
            var command = new DeleteCartCommand(id);
            await _mediator.Send(command);

            return Ok(new { Message = "Cart deleted successfully" });
        }

        /// <summary>
        /// Converts a cart into a sale (checkout process).
        /// </summary>
        /// <param name="id">The unique identifier of the cart to be checked out.</param>
        /// <returns>The sale created from the checkout process.</returns>
        /// <response code="201">Returns the created sale.</response>
        /// <response code="404">If the cart is not found or cannot be converted to a sale.</response>
        [HttpPost("{id}/checkout")]
        [ProducesResponseType(typeof(SaleDto), 201)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SaleDto>> CheckoutCart(int id)
        {
            var command = new ConvertCartToSaleCommand(id);
            var result = await _mediator.Send(command);

            if (result is null)
            {
                return NotFound(new { Message = "Cart not found or could not be converted to a sale" });
            }

            return CreatedAtAction("GetSaleById", "Sales", new { id = result.Id }, result);
        }
    }
}