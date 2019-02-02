using System;
using Api.Hub.Domain.DTOs;

namespace Api.Hub.Domain.GameDomain
{
    public class Player
    {
        public Bubble Bubble { get; set; }
        public string BeatenBy { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsNpc { get; set; } = false;
        public Guid Guid { get; set; }
    
        public Player(string connectionId, BubbleDto bubbleDto)
        {
            ConnectionId = connectionId;
            Bubble = new Bubble(bubbleDto);
        }

        public Player(){}
    }
}
