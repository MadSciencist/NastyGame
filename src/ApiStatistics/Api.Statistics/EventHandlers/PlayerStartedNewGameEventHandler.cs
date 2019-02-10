using Api.Common.Infrastructure;
using Api.Common.Messaging.Abstractions;
using Api.Statistics.Events;
using Api.Statistics.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api.Statistics.EventHandlers
{
    public class PlayerStartedNewGameEventHandler : IIntegrationEventHandler<PlayerStartedNewGameEvent>
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly ILogger<UpdateUserDeadthsEventHandler> _logger;

        public PlayerStartedNewGameEventHandler(IStatisticsRepository statisticsRepository, ILogger<UpdateUserDeadthsEventHandler> logger)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;
        }

        public async Task Handle(PlayerStartedNewGameEvent @event)
        {
            _logger.LogDebugAsJson(@event);
            await _statisticsRepository.SaveNewGameAsync(@event.UserId, @event.JoineDate);
        }
    }
}
