using Api.Common.Messaging.Abstractions;
using Api.Statistics.Events;
using System;
using System.Threading.Tasks;
using Api.Statistics.Infrastructure.Repository;

namespace Api.Statistics.EventHandlers
{
    public class PlayerStartedNewGameEventHandler : IIntegrationEventHandler<PlayerStartedNewGameEvent>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public PlayerStartedNewGameEventHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task Handle(PlayerStartedNewGameEvent @event)
        {
            await _statisticsRepository.SaveNewGameAsync(@event.UserId, @event.JoineDate);
        }
    }
}
