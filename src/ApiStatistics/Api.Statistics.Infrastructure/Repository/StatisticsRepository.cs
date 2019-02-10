using Api.Statistics.Domain;
using Api.Statistics.Domain.Entity;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Api.Statistics.Infrastructure.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly IOptions<ApiStatisticsConfiguration> _config;

        public StatisticsRepository(IOptions<ApiStatisticsConfiguration> config)
        {
            _config = config;
        }

        public async Task SaveNewGameAsync(int playerId, DateTime startTime)
        {
            try
            {
                using (var connection = new SqlConnection(_config.Value.ConnectionString))
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
                using (var connection = new SqlConnection(_config.Value.ConnectionString))
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
                using (var connection = new SqlConnection(_config.Value.ConnectionString))
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
