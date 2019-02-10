using System;

namespace Api.Statistics.Domain.Entity
{
    public class UserGamesEntity
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int KilledById { get; set; }
        public string KilledBy { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
