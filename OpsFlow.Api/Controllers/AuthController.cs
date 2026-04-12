using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpsFlow.Application.Common.Results;
using OpsFlow.Application.Users.Commands.Login;
using OpsFlow.Application.Users.Commands.Logout;
using OpsFlow.Application.Users.Commands.RefreshToken;
using OpsFlow.Application.Users.Commands.Register;
using OpsFlow.Application.Users.Dtos;
using OpsFlow.Application.Users.Queries.GetMyProfile;

namespace OpsFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a user record.
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// User log in and return Jwt token.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            AuthTokenResponseDto result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Refreshes access token.
        /// </summary>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            AuthTokenResponseDto result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Log user out.
        /// </summary>
        public async Task<IActionResult> Logout(LogoutCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Fetch logged in users info.
        /// </summary
        [HttpGet("current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser(GetMyProfileQuery query)
        {
            GetUserDetailResponseDto result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}