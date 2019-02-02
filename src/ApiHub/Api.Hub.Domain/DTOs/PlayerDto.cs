using Api.Hub.Domain.GameDomain;
using System;
using System.Collections.Generic;

namespace Api.Hub.Domain.DTOs
{
    /// <summary>
    /// This class is used to deliver summary to client in case of win/lost
    /// </summary>
    public class PlayerDto
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public IList<string> Victims { get; set; }
        public string KilledBy { get; set; }
        public DateTime JoinedTime { get; set; }

        public PlayerDto(Player player)
        {
            Score = player.Score;
            Victims = player.Victims;
            KilledBy = player.KilledBy;
            JoinedTime = player.JoinedTime;
            ConnectionId = player.ConnectionId;
            Name = player.Name;
        }

        public PlayerDto() { }
    }
}
