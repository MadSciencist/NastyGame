using System.Threading.Tasks;
using Api.Identity.Domain;
using Api.Identity.Domain.DTOs;

namespace Api.Identity.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get basic user informations
        /// </summary>
        /// <param name="login">Should be unique</param>
        /// <returns></returns>
        Task<UserEntity> GetUserByLogin(string login);

        /// <summary>
        /// This includes all user info, including addresses
        /// </summary>
        /// <param name="login">Should be unique</param>
        /// <returns></returns>
        Task<UserEntity> GetFullUserInfo(string login);

        /// <summary>
        /// Create new user 
        /// </summary>
        /// <param name="user">Should be unique</param>
        /// <returns></returns>
        Task<UserEntity> CreateUser(RegisterDto user);
    }
}
