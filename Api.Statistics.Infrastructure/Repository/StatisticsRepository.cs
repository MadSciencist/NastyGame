﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Api.Statistics.Domain.Entity;
using Dapper;
using System.Linq;

namespace Api.Statistics.Infrastructure.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly string _connectionString = @"Server=(LocalDb)\MSSQLLocalDB;Database=StatisticsServiceDb;";

        public async Task SaveNewGameAsync(int playerId, DateTime startTime)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.QueryAsync<UserGamesEntity>("InsertGame",
                        new {PlayerId = playerId, StartTime = startTime},
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task EndGameSession(int playerId, int killedById, string killedBy, DateTime endTime)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.QueryAsync<UserGamesEntity>("EndGameSession",
                        new {UserId = playerId, KilledById = killedById, KilledBy = killedBy, EndTime = endTime},
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public async Task AddUserVictim(int playerId, int victimId, string victimName)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.QueryAsync<GameVictimsEntity>("InsertVictim",
                        new { UserId = playerId, VictimId = victimId, VictimName = victimName },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
