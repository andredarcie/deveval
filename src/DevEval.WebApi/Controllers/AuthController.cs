using DevEval.Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevEval.WebApi.Controllers
{
    /// <summary>
    /// API for authentication, providing endpoints for user login.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator instance for handling requests.</param>
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Authenticates a user and returns a token if the login is successful.
        /// </summary>
        /// <param name="command">The login command containing user credentials.</param>
        /// <returns>A token if the credentials are valid, or an error message otherwise.</returns>
        /// <response code="200">Returns the authentication token.</response>
        /// <response code="401">If the credentials are invalid.</response>
        /// <response code="400">If there is an error processing the request.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(new { token });
        }
    }
}