using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevEval.Common.Helpers.Pagination;
using DevEval.Application.Sales.Dtos;
using DevEval.Application.Sales.Commands;
using DevEval.Application.Sales.Queries;
using Microsoft.AspNetCore.Authorization;

namespace DevEval.WebApi.Controllers
{
    /// <summary>
    /// API for managing Sales, including retrieval, creation, updating, and deletion of sales.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator instance for handling requests.</param>
        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieve a paginated list of Sales.
        /// </summary>
        /// <param name="_page">The page number to retrieve (default: 1).</param>
        /// <param name="_size">The number of records per page (default: 10).</param>
        /// <param name="_order">Sorting criteria (e.g., "date asc, total desc").</param>
        /// <returns>A paginated list of Sales.</returns>
        /// <response code="200">Returns the paginated list of Sales.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<SaleDto>), 200)]
        public async Task<ActionResult<PaginatedResult<SaleDto>>> GetSales(
            [FromQuery] int _page = 1,
            [FromQuery] int _size = 10,
            [FromQuery] string _order = "")
        {
            var query = new GetSalesQuery(new PaginationParameters
            {
                Page = _page,
                PageSize = _size,
                OrderBy = _order
            });

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retrieve a specific Sale by ID.
        /// </summary>
        /// <param name="id">The ID of the Sale to retrieve.</param>
        /// <returns>The Sale with the specified ID.</returns>
        /// <response code="200">Returns the Sale.</response>
        /// <response code="404">If the Sale is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SaleDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SaleDto>> GetSaleById(Guid id)
        {
            var query = new GetSaleByIdQuery(id);
            var result = await _mediator.Send(query);
            return result is not null ? Ok(result) : NotFound(new { Message = "Sale not found" });
        }

        /// <summary>
        /// Add a new Sale.
        /// </summary>
        /// <param name="command">The command containing the Sale details.</param>
        /// <returns>The created Sale.</returns>
        /// <response code="201">Returns the created Sale.</response>
        [HttpPost]
        [ProducesResponseType(typeof(SaleDto), 201)]
        public async Task<ActionResult<SaleDto>> CreateSale([FromBody] CreateSaleCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetSaleById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Update an existing Sale by ID.
        /// </summary>
        /// <param name="id">The ID of the Sale to update.</param>
        /// <param name="command">The command containing the updated Sale details.</param>
        /// <returns>The updated Sale.</returns>
        /// <response code="200">Returns the updated Sale.</response>
        /// <response code="400">If the ID in the path does not match the ID in the body.</response>
        /// <response code="404">If the Sale is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SaleDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SaleDto>> UpdateSale(Guid id, [FromBody] UpdateSaleCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { Message = "ID in path and body do not match" });

            var result = await _mediator.Send(command);
            return result is not null ? Ok(result) : NotFound(new { Message = "Sale not found" });
        }

        /// <summary>
        /// Delete a Sale by ID.
        /// </summary>
        /// <param name="id">The ID of the Sale to delete.</param>
        /// <response code="200">If the Sale is deleted successfully.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> DeleteSale(Guid id)
        {
            var command = new DeleteSaleCommand(id);
            await _mediator.Send(command);

            return Ok(new { Message = "Sale deleted successfully" });
        }

        /// <summary>
        /// Cancel a specific item in a Sale.
        /// </summary>
        /// <param name="saleId">The ID of the Sale.</param>
        /// <param name="itemId">The ID of the item to cancel.</param>
        /// <param name="reason">The reason for cancellation.</param>
        /// <returns>A confirmation message if the cancellation is successful.</returns>
        /// <response code="200">If the item is cancelled successfully.</response>
        /// <response code="400">If the cancellation fails.</response>
        [HttpPut("{saleId}/items/{itemId}/cancel")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CancelSaleItem(Guid saleId, Guid itemId, [FromBody] string reason)
        {
            var command = new CancelSaleItemCommand(saleId, itemId, reason);
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok(new { Message = "Item cancelled successfully." });
            }

            return BadRequest(new { Message = "Failed to cancel the item." });
        }
    }
}