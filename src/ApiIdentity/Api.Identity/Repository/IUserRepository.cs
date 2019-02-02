using Api.Identity.Domain;
using Api.Identity.Domain.DTOs;

namespace Api.Identity.Repository
{
    public interface IUserRepository
    {
        UserEntity GetUserByLogin(string login);
        UserEntity CreateUser(RegisterDto user);
    }
}
