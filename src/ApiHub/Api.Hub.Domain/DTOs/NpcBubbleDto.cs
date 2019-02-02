using Api.Hub.Domain.GameDomain;
using System;

namespace Api.Hub.Domain.DTOs
{
    public class NpcBubbleDto
    {
        // TODO merge it somehow with EnemyBubbleDto (superclass or something)
        public Point2D Position { get; set; }
        public double Radius { get; set; }
        public Guid Guid { get; set; }

        public NpcBubbleDto(NpcBubble npcBubble)
        {
            Position = npcBubble.Bubble.Position;
            Radius = npcBubble.Bubble.Radius;
            Guid = npcBubble.Guid;
        }

        // Needed for MessagePack serialization
        public NpcBubbleDto(){ }
    }
}
