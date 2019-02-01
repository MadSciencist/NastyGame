using Api.Identity.Models;
using Api.Identity.Models.DTOs;

namespace Api.Identity.Repository
{
    public interface IUserRepository
    {
        UserEntity GetUserByLogin(string login);
        UserEntity CreateUser(RegisterDto user);
    }
}
