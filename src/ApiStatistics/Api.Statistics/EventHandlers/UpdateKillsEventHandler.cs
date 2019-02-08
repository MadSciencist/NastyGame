using Api.Common.Messaging.Abstractions;
using Api.Statistics.Events;
using System;
using System.Threading.Tasks;

namespace Api.Statistics.EventHandlers
{
    public class UpdateKillsEventHandler : IIntegrationEventHandler<UpdateUserKillsEvent>
    {
        public Task Handle(UpdateUserKillsEvent @event)
        {
            Console.WriteLine($"Update Kills: {@event.UserId} {@event.Victim}");

            return Task.CompletedTask;
        }
    }
}
