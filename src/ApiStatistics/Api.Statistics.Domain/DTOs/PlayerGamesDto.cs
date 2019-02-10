using System;

namespace Api.Statistics.Domain.DTOs
{
    public class PlayerGamesDto
    {
        public int GameId { get; set; }
        public int MurdererId { get; set; }
        public string MurdererNickname { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan? GameDuration { get; set; }
    }
}
