using Api.Common.Infrastructure;
using Api.Common.Messaging.Abstractions;
using Api.Statistics.Events;
using Api.Statistics.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api.Statistics.EventHandlers
{
    public class UpdateKillsEventHandler : IIntegrationEventHandler<UpdateUserKillsEvent>
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly ILogger<UpdateUserDeadthsEventHandler> _logger;

        public UpdateKillsEventHandler(IStatisticsRepository statisticsRepository, ILogger<UpdateUserDeadthsEventHandler> logger)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;
        }

        public async Task Handle(UpdateUserKillsEvent @event)
        {
            _logger.LogDebugAsJson(@event);
            await _statisticsRepository.AddUserVictim(@event.UserId, @event.VictimId, @event.Victim);
        }
    }
}
