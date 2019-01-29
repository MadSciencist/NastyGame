using System.Net;
using Api.Identity.Infrastructure;
using Api.Identity.Models.DTOs;
using Api.Identity.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IPasswordHasher _hasher;

        public AccountController(IUserRepository userRepository, ITokenBuilder tokenBuilder, IUserAuthenticator authenticator, IPasswordHasher hasher)
        {
            _userRepository = userRepository;
            _tokenBuilder = tokenBuilder;
            _authenticator = authenticator;
            _hasher = hasher;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userRepository.GetUserByLogin(loginDto.Login);

            if (user == null)
            {
                return Unauthorized(new { Errors = new {title = "User not found"}});
            }

            if (_authenticator.Authenticate(loginDto.Password, user.Password))
            {
                var token = _tokenBuilder.BuildToken(user);

                return Ok(new { user = new UserDto(user), token });
            }

            return Unauthorized(new { Errors = new { title = "Password is not correct" } });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            var user = _userRepository.GetUserByLogin(registerDto.Login);

            if (user != null)
            {
                return BadRequest(new { Errors = new { title = "User with this login already exists" } });
            }

            var hashedPassword = _hasher.CreateHashString(registerDto.Password);
            registerDto.Password = hashedPassword;

            var createdUser = _userRepository.CreateUser(registerDto);

            if (createdUser == null)
            {
                return BadRequest(new { Errors = new { title = "Incorrect input data" } });
            }

            return Created(nameof(Register), new { created = new UserDto(createdUser) });
        }
    }
}