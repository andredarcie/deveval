using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevEval.Application.Users.Commands;
using DevEval.Common.Helpers.Pagination;
using DevEval.Application.Users.Queries;
using DevEval.Application.Users.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace DevEval.WebApi.Controllers
{
    /// <summary>
    /// API for managing Users, including retrieval, creation, updating, and deletion.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator instance for handling requests.</param>
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieve a paginated list of Users.
        /// </summary>
        /// <param name="_page">The page number to retrieve (default: 1).</param>
        /// <param name="_size">The number of records per page (default: 10).</param>
        /// <param name="_order">Sorting criteria (e.g., "name asc, email desc").</param>
        /// <returns>A paginated list of Users.</returns>
        /// <response code="200">Returns the paginated list of Users.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<UserDto>), 200)]
        public async Task<ActionResult<PaginatedResult<UserDto>>> GetUsers(
            [FromQuery] int _page = 1,
            [FromQuery] int _size = 10,
            [FromQuery] string _order = "")
        {
            var query = new GetUsersQuery(new PaginationParameters
            {
                Page = _page,
                PageSize = _size,
                OrderBy = _order
            });

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retrieve a specific User by ID.
        /// </summary>
        /// <param name="id">The ID of the User to retrieve.</param>
        /// <returns>The User with the specified ID.</returns>
        /// <response code="200">Returns the User.</response>
        /// <response code="404">If the User is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);
            return result is not null ? Ok(result) : NotFound(new { Message = "User not found" });
        }

        /// <summary>
        /// Add a new User.
        /// </summary>
        /// <param name="command">The command containing the User details.</param>
        /// <returns>The created User.</returns>
        /// <response code="201">Returns the created User.</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), 201)]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Update an existing User by ID.
        /// </summary>
        /// <param name="id">The ID of the User to update.</param>
        /// <param name="command">The command containing the updated User details.</param>
        /// <returns>The updated User.</returns>
        /// <response code="200">Returns the updated User.</response>
        /// <response code="400">If the ID in the path does not match the ID in the body.</response>
        /// <response code="404">If the User is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { Message = "ID in path and body do not match" });

            var result = await _mediator.Send(command);
            return result is not null ? Ok(result) : NotFound(new { Message = "User not found" });
        }

        /// <summary>
        /// Delete a User by ID.
        /// </summary>
        /// <param name="id">The ID of the User to delete.</param>
        /// <response code="200">If the User is deleted successfully.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand(id);
            await _mediator.Send(command);

            return Ok(new { Message = "User deleted successfully" });
        }
    }
}