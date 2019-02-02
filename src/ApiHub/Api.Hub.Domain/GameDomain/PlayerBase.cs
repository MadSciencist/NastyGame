using System;
using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.GameConfig;

namespace Api.Hub.Domain.GameDomain
{
    public class PlayerBase
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public Bubble Bubble { get; set; }
        public bool IsNpc { get; set; } = false;
        public bool IsDown { get; set; } = false;
    
        public PlayerBase(string connectionId, BubbleDto bubbleDto)
        {
            ConnectionId = connectionId;
            Bubble = new Bubble(bubbleDto);
        }

        public PlayerBase(){ }

        public bool TryKill(PlayerBase otherPlayer)
        {
            var distance = Bubble.GetDistance(otherPlayer.Bubble);

            if (distance < Bubble.Radius + otherPlayer.Bubble.Radius)
            {
                var totalArea = Math.PI * Bubble.Radius * Bubble.Radius + Math.PI * otherPlayer.Bubble.Radius * otherPlayer.Bubble.Radius;
                if(Bubble.Radius < BubbleConfig.MaxRadius) Bubble.Radius = Math.Sqrt(totalArea / Math.PI);

                return true;
            }

            return false;
        }
    }
}
