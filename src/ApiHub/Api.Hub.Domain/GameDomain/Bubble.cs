using System;
using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.GameConfig;

namespace Api.Hub.Domain.GameDomain
{
    public class Bubble
    {
        public Point2D Position { get; set; }
        public double Radius { get; set; }

        public Bubble(BubbleDto bubbleDto)
        {
            Position = bubbleDto.Position;
            Radius = bubbleDto.Radius;
        }

        public Bubble(Bubble bubble)
        {
            Position = bubble.Position;
            Radius = bubble.Radius;
        }

        public Bubble()
        {
        }

        public bool CanBeat(Bubble otherBubble)
        {
            var distance = GetDistance(otherBubble);

            if (distance < Radius + otherBubble.Radius)
            {
                var totalArea = Math.PI * Radius * Radius + Math.PI * otherBubble.Radius * otherBubble.Radius;
                if(Radius < BubbleConfig.MaxRadius) Radius = Math.Sqrt(totalArea / Math.PI);

                Console.WriteLine("Killed");
                return true;
            }

            return false;
        }

        public double GetDistance(Bubble other)
        {
            var deltaX = Position.x - other.Position.x;
            var deltaY = Position.y - other.Position.y;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}

