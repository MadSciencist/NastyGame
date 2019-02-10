using Api.Common.Messaging.Abstractions;
using Api.Statistics.Events;
using Api.Statistics.Infrastructure.Repository;
using System.Threading.Tasks;

namespace Api.Statistics.EventHandlers
{
    public class UpdateKillsEventHandler : IIntegrationEventHandler<UpdateUserKillsEvent>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public UpdateKillsEventHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task Handle(UpdateUserKillsEvent @event)
        {
            await _statisticsRepository.AddUserVictim(@event.UserId, @event.VictimId, @event.Victim);
        }
    }
}
