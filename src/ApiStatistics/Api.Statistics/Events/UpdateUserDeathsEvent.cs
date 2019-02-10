using Api.Common.Messaging.Abstractions;
using System;

namespace Api.Statistics.Events
{
    public class UpdateUserDeathsEvent : IntegrationEvent
    {
        public int UserId { get; set; }
        public int KilledById { get; set; }
        public string KilledBy { get; set; }
        public DateTime EndTime { get; set; }

        public UpdateUserDeathsEvent(int userId, int killedById, string killedBy,  DateTime gameTime)
        {
            UserId = userId;
            KilledById = killedById;
            KilledBy = killedBy;
            EndTime = gameTime;
        }
    }
}
