using Api.Common.Messaging.Abstractions;
using System;

namespace Api.Hub.Events
{
    public class UpdateUserDeathsEvent : IntegrationEvent
    {
        public int UserId { get; set; }
        public string KilledBy { get; set; }
        public TimeSpan GameTime { get; set; }


        public UpdateUserDeathsEvent(int userId, string killedBy, TimeSpan gameTime)
        {
            UserId = userId;
            KilledBy = killedBy;
            GameTime = gameTime;
        }
    }
}
