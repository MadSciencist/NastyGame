using Api.Hub.Domain.DTOs;
using System;

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

        public Bubble() { }

        public double GetDistance(Bubble other)
        {
            var deltaX = Position.x - other.Position.x;
            var deltaY = Position.y - other.Position.y;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}

