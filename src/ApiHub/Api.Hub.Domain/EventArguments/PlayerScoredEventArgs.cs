using Api.Hub.Domain.GameDomain;
using System;

namespace Api.Hub.Domain.EventArguments
{
    public class PlayerScoredEventArgs : EventArgs
    {
        public Player Murderer { get; set; }
        public int VictimId { get; set; }
    }
}
