using Api.Identity.Domain;
using Api.Identity.Domain.DTOs;
using Api.Identity.Services;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Identity.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IPasswordHasher _hasher;
        private readonly ILogger<UserRepository> _logger;
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration, IPasswordHasher hasher, ILogger<UserRepository> logger)
        {
            _hasher = hasher;
            _logger = logger;
            _connectionString = configuration["ConnectionStrings:MsSql"];
        }

        public async Task<UserEntity> GetUserByLogin(string login)
        {
            const string query = "SELECT TOP(1) * FROM Users WHERE Login = @Login";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var user = await connection.QueryFirstOrDefaultAsync<UserEntity>(query, new {Login = login});

                    return user;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while querying user data: {login}");
                return null;
            }
        }

        public async Task<UserEntity> GetFullUserInfo(string login)
        {
            const string query = @"SELECT TOP(1) * FROM Users LEFT JOIN Addresses ON Users.UserId = Addresses.UserId WHERE Login = @Login";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var lookup = new Dictionary<int, UserEntity>();

                    var users = await connection.QueryAsync<UserEntity, AddressEntity, UserEntity>(query, (user, address) =>
                    {
                        if (!lookup.TryGetValue(user.UserId, out var userEntity))
                        {
                            lookup.Add(user.UserId, userEntity = user);
                        }

                        if (userEntity.Addresses == null)
                        {
                            user.Addresses = new List<AddressEntity>();
                        }
                        userEntity.Addresses?.Add(address);

                        return userEntity;

                    }, splitOn: "UserId", param: new { Login = login });

                    return users.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while querying user data: {login}");
                return null;
            }
        }

        public async Task<UserEntity> CreateUser(RegisterDto user)
        {
            var passwordHash = _hasher.CreateHashString(user.Password);

            const string query = @"INSERT INTO Users(Login, Password, Name, LastName, BirthDate, JoinDate, Email) VALUES (@Login, @Password, @Name, @LastName, @BirthDate, @JoinDate, @Email)";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rowsAffected = await connection.ExecuteAsync(query,
                        new
                        {
                            user.Login, passwordHash, user.Name, user.LastName, user.BirthDate, JoinDate = DateTime.Now,
                            user.Email
                        });

                    if (rowsAffected == 0)
                    {
                        _logger.LogError($"Error while creating user: no rows affected");
                        return null;
                    }
                }
            }
            catch (SqlException e) // probably trying to insert some NULLs
            {
                _logger.LogError(e, $"Error while creating user: {user.Login}");
                return null;
            }

            return await GetUserByLogin(user.Login);
        }
    }
}
