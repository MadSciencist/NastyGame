using Api.Identity.Domain;

namespace Api.Identity.Services
{
    public interface ITokenBuilder
    {
        string BuildToken(UserEntity user);
    }
}
