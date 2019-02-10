using System;
using System.Collections.Generic;

namespace Api.Hub.Domain.GameDomain
{
    public class Player : PlayerBase
    {
        public bool IsAuthenticated { get; set; }
        public int GlobalId { get; set; }
        public string GlobalName { get; set; }
        public int Score { get; set; }
        public IList<string> Victims { get; set; }
        public int KilledById { get; set; }
        public string KilledBy { get; set; }
        public DateTime JoinedTime { get; set; }
    }
}
