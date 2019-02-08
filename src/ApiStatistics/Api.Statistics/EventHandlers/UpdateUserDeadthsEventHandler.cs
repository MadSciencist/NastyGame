using Api.Common.Messaging.Abstractions;
using Api.Statistics.Events;
using System;
using System.Threading.Tasks;

namespace Api.Statistics.EventHandlers
{
    public class UpdateUserDeadthsEventHandler : IIntegrationEventHandler<UpdateUserDeathsEvent>
    {
        public Task Handle(UpdateUserDeathsEvent @event)
        {
            Console.WriteLine($"Update deaths: {@event.UserId} {@event.KilledBy}");

            return Task.CompletedTask;
        }
    }
}
