using Api.Hub.Domain.GameDomain;
using System;

namespace Api.Hub.Domain.DTOs
{
    public class NpcBubbleDto
    {
        public Bubble Bubble { get; set; }
        public Guid Guid { get; set; }

        public NpcBubbleDto(NpcBubble npcBubble)
        {
            Bubble = new Bubble(npcBubble.Bubble);

            Guid = npcBubble.Guid;
        }

        // Needed for MessagePack serialization
        public NpcBubbleDto(){ }
    }
}
