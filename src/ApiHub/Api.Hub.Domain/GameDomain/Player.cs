using System;
using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.GameConfig;

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
        public bool IsDown { get; set; }
    
        public Player(string connectionId, BubbleDto bubbleDto)
        {
            ConnectionId = connectionId;
            Bubble = new Bubble(bubbleDto);
        }

        public Player(){}

        public bool CanBeat(Player otherPlayer)
        {
            var distance = Bubble.GetDistance(otherPlayer.Bubble);

            if (distance < Bubble.Radius + otherPlayer.Bubble.Radius)
            {
                //var totalArea = Math.PI * Radius * Radius + Math.PI * otherBubble.Radius * otherBubble.Radius;
                //if(Radius < BubbleConfig.MaxRadius) Radius = Math.Sqrt(totalArea / Math.PI);

                if (Bubble.Radius < BubbleConfig.MaxRadius) Bubble.Radius *= 1.05;

               // Console.WriteLine("Killed");
                return true;
            }

            return false;
        }
    }
}
