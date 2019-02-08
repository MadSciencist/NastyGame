using Api.Common.Messaging.Abstractions;

namespace Api.Statistics.Events
{
    public class UpdateUserKillsEvent : IntegrationEvent
    {
        public int UserId { get; set; }
        public string Victim { get; set; }

        public UpdateUserKillsEvent(int userId, string victim)
        {
            UserId = userId;
            Victim = victim;
        }
    }
}
