using System;
using Dapper;
using Api.Identity.Models;
using System.Data.SqlClient;
using Api.Identity.Infrastructure;
using Microsoft.Extensions.Configuration;
using Api.Identity.Models.DTOs;

namespace Api.Identity.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IPasswordHasher _hasher;
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration, IPasswordHasher hasher)
        {
            _hasher = hasher;
            _connectionString = configuration["ConnectionStrings:MsSql"];
        }

        public UserEntity GetUserByLogin(string login)
        {
            const string query = "SELECT * FROM Users WHERE Login = @Login";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var user = connection.QueryFirstOrDefault<UserEntity>(query, new {Login = login});

                    return user;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UserEntity CreateUser(RegisterDto user)
        {
            var passwordHash = _hasher.CreateHashString(user.Password);

            const string query = @"INSERT INTO Users(Login, Password, Name, LastName, BirthDate, JoinDate, Email) VALUES (@Login, @Password, @Name, @LastName, @BirthDate, @JoinDate, @Email)";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var rowsAffected = connection.Execute(query,
                        new
                        {
                            user.Login, passwordHash, user.Name, user.LastName, user.BirthDate, JoinDate = DateTime.Now,
                            user.Email
                        });

                    if (rowsAffected == 0)
                    {
                        return null;
                    }
                }
            }
            catch (SqlException) // probably trying to insert some NULLs
            {
                return null;
            }

            return GetUserByLogin(user.Login);
        }
    }
}
