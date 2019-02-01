using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.GameConfig;
using Api.Hub.Domain.GameDomain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Hub.Domain.Services
{
    public class NpcService : INpcService
    {
        private readonly IList<NpcBubble> _npcs;

        public NpcService()
        {
            _npcs = new List<NpcBubble>(BubbleConfig.MaxNpcs);
            GenerateNpcs(BubbleConfig.MaxNpcs);
        }

        public IList<NpcBubble> GetNpcs()
        {
            if (_npcs.Count < BubbleConfig.MinNpcs)
            {
                var random = new Random();

                var countToAdd = random.Next((BubbleConfig.MaxNpcs - _npcs.Count) / 2,
                    BubbleConfig.MaxNpcs - _npcs.Count); // add something between half of missing to full set

                GenerateNpcs(countToAdd);
            }

            return _npcs;
        }

        public void KillNpc(Guid guid)
        {
            var victim = _npcs.FirstOrDefault(v => v.Guid == guid);
            if(victim != null) _npcs.Remove(victim);
        }

        private void GenerateNpcs(int count)
        {
            var random = new Random();

            for (var i = 0; i < count; i++)
            {
                var radius = random.Next(BubbleConfig.MinRadius, BubbleConfig.MaxRadius);
                var posX = random.Next(0, CanvasConfig.CanvasWidth);
                var posY = random.Next(0, CanvasConfig.CanvasHeight);
                var position = new Point2D(posX, posY);

                _npcs.Add(new NpcBubble
                    { Guid = Guid.NewGuid(), Bubble = new Bubble {Radius = radius, Position = position}});
            }
        }
    }
}
