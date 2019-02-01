using Api.Identity.Models;

namespace Api.Identity.Infrastructure
{
    public interface ITokenBuilder
    {
        string BuildToken(UserEntity user);
    }
}
