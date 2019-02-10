using Api.Common.Messaging.Abstractions;
using Api.Statistics.Events;
using Api.Statistics.Infrastructure.Repository;
using System.Threading.Tasks;

namespace Api.Statistics.EventHandlers
{
    public class UpdateUserDeadthsEventHandler : IIntegrationEventHandler<UpdateUserDeathsEvent>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public UpdateUserDeadthsEventHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task Handle(UpdateUserDeathsEvent @event)
        {
            await _statisticsRepository.EndGameSession(@event.UserId, @event.KilledById, @event.KilledBy, @event.EndTime);
        }
    }
}
