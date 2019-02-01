using Api.Identity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Identity.Infrastructure
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly IConfiguration _config;

        public TokenBuilder(IConfiguration config)
        {
            _config = config;
        }

        public string BuildToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthenticationJwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["AuthenticationJwt:Issuer"],
                _config["AuthenticationJwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_config["AuthenticationJwt:ValidTimeMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
