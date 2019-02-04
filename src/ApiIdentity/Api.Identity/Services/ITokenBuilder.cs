using System;
using Api.Identity.Domain;

namespace Api.Identity.Services
{
    public interface ITokenBuilder
    {
        (string token, DateTime expring) BuildToken(UserEntity user);
    }
}
