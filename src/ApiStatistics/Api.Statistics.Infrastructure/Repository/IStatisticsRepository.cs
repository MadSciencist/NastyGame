using System;
using System.Threading.Tasks;

namespace Api.Statistics.Infrastructure.Repository
{
    public interface IStatisticsRepository
    {
        Task SaveNewGameAsync(int playerId, DateTime startTime);
        Task EndGameSession(int playerId, int killedById, string killedBy, DateTime endTime);
        Task AddUserVictim(int playerId, int victimId, string victimName);
    }
}