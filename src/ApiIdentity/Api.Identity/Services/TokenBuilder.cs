using Api.Identity.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Identity.Services
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly IConfiguration _config;

        public TokenBuilder(IConfiguration config)
        {
            _config = config;
        }

        public (string token, DateTime expring) BuildToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthenticationJwt:Key"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(double.Parse(_config["AuthenticationJwt:ValidTimeMinutes"]));

            var token = new JwtSecurityToken(
                _config["AuthenticationJwt:Issuer"],
                _config["AuthenticationJwt:Audience"],
                claims,
                expires,
                signingCredentials: signingCredentials);

            return (new JwtSecurityTokenHandler().WriteToken(token), expires);
        }
    }
}
