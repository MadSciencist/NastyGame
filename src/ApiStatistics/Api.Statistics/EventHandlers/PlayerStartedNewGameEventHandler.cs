using Api.Common.Messaging.Abstractions;
using Api.Statistics.Events;
using System;
using System.Threading.Tasks;

namespace Api.Statistics.EventHandlers
{
    public class PlayerStartedNewGameEventHandler : IIntegrationEventHandler<PlayerStartedNewGameEvent>
    {
        public Task Handle(PlayerStartedNewGameEvent @event)
        {
            Console.WriteLine($"new game: {@event.UserId} {@event.JoineDate}");

            return Task.CompletedTask;
        }
    }
}
