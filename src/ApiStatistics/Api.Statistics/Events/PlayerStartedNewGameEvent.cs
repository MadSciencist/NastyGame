using Api.Common.Messaging.Abstractions;
using System;

namespace Api.Statistics.Events
{
    public class PlayerStartedNewGameEvent : IntegrationEvent
    {
        public int UserId { get; set; }
        public DateTime JoineDate { get; set; }

        public PlayerStartedNewGameEvent(int userId, DateTime joineDate)
        {
            UserId = userId;
            JoineDate = joineDate;
        }
    }
}
