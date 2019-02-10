using Api.Common.Messaging.Abstractions;

namespace Api.Hub.Events
{
    public class UpdateUserKillsEvent : IntegrationEvent
    {
        public int UserId { get; set; }
        public int VictimId { get; set; }
        public string Victim { get; set; }

        public UpdateUserKillsEvent(int userId, int victimId, string victim)
        {
            UserId = userId;
            VictimId = victimId;
            Victim = victim;
        }
    }
}
