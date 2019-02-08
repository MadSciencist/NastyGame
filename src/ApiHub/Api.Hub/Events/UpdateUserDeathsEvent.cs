using Api.Common.Messaging.Abstractions;
using System;

namespace Api.Hub.Events
{
    public class UpdateUserDeathsEvent : IntegrationEvent
    {
        public int UserId { get; set; }
        public string KilledBy { get; set; }
        public TimeSpan GameTime { get; set; }


        public UpdateUserDeathsEvent(string killedBy, int userId, TimeSpan gameTime)
        {
            KilledBy = killedBy;
            UserId = userId;
            GameTime = gameTime;
        }
    }
}
