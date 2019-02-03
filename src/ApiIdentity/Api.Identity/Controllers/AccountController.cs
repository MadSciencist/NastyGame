using Api.Identity.Domain.DTOs;
using Api.Identity.Repository;
using Api.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace Api.Identity.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IUserAuthenticator _authenticator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserRepository userRepository, ITokenBuilder tokenBuilder, IUserAuthenticator authenticator, ILogger<AccountController> logger)
        {
            _userRepository = userRepository;
            _tokenBuilder = tokenBuilder;
            _authenticator = authenticator;
            _logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByLogin(loginDto.Login);

            if (user == null)
            {
                _logger.LogInformation($"User: {loginDto.Login} not found");
                return Unauthorized(new { Errors = new {title = "User not found"}});
            }

            if (_authenticator.Authenticate(loginDto.Password, user.Password))
            {
                var token = _tokenBuilder.BuildToken(user);
                _logger.LogInformation($"Created token for user: {loginDto.Login}");
                return Ok(new { user = new UserDto(user), token });
            }

            _logger.LogInformation($"Unauthorized: {loginDto.Login}");
            return Unauthorized(new { Errors = new { title = "Password is not correct" } });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = await _userRepository.GetUserByLogin(registerDto.Login);

            if (user != null)
            {
                _logger.LogInformation($"User: {registerDto.Login} already exists");
                return BadRequest(new { Errors = new { title = "User with this login already exists" } });
            }

            var createdUser = await _userRepository.CreateUser(registerDto);

            if (createdUser == null)
            {
                _logger.LogInformation($"User: {registerDto.Login} incorrect input data");
                return BadRequest(new { Errors = new { title = "Incorrect input data" } });
            }

            _logger.LogInformation($"Created uesr: {registerDto.Login}");
            return Created(nameof(Register), new { created = new UserDto(createdUser) });
        }
    }
}