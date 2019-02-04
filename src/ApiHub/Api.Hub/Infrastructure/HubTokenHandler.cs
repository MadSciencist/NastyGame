using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Api.Hub.Infrastructure
{
    /// <summary>
    /// This custom class is needed to provide access to hub to both authenticated and annonymous users
    /// ASP NET Core does not verify the token on its own if there is no Authorize attribute in its pipeline
    /// And adding custom event handlers (like OnMEssageReceived) doesnt help - this is the best solution I found
    /// </summary>
    public class HubTokenHandler : IHubTokenHandler
    {
        private readonly IConfiguration _configuration;

        public HubTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (bool isAuth, int id, string name) GetClaimValues(HubCallerContext context)
        {
            var rawToken = context.GetHttpContext().Request.HttpContext.Request.Query["access_token"].ToString();

            if (string.IsNullOrEmpty(rawToken)) return (false, 0, null);

            // TODO move  this somewhere to share it with startup
            var jwtKey = _configuration["AuthenticationJwt:Key"];
            var jwtIssuer = _configuration["AuthenticationJwt:Issuer"];
            var jwtAudience = _configuration["AuthenticationJwt:Audience"];

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ClockSkew = TimeSpan.FromMinutes(0),
            };

            var decodedToken = new JwtSecurityTokenHandler().ValidateToken(rawToken, validationParams, out var token);

            var name = decodedToken?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var nameIdentifier = decodedToken?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

            if (int.TryParse(nameIdentifier, out var id))
            {
                return (true, id, name);
            }

            return (false, 0, null);
        }
    }
}
