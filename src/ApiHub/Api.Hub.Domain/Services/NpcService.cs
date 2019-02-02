using Api.Hub.Domain.GameConfig;
using Api.Hub.Domain.GameDomain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Hub.Domain.Services
{
    public class NpcService : INpcService
    {
        public IEnumerable<Player> GetDefaultCountOfNpcs() => GenerateNpcs(BubbleConfig.MaxNpcs);

        public int GetCountOfNeededNpcs(IEnumerable<Player> players)
        {
            // ReSharper disable once RemoveToList.2
            var npcsNow = players.ToList().Count(x => x.IsNpc);

            return BubbleConfig.MaxNpcs - npcsNow; // generate only missing NPCs - probably this will be about 1

            // Generate random number of NPCs - need to test both approaches
            //if (npcsNow < BubbleConfig.MinNpcs)
            //{
            //    var random = new Random(333);
            //    var countToAdd = random.Next((BubbleConfig.MaxNpcs - npcsNow) / 2,
            //        BubbleConfig.MaxNpcs - npcsNow); // add something between half of missing to full set

            //    return countToAdd;
            //}
        }

        public IEnumerable<Player> GenerateNpcs(int count)
        {
            var npcs = new List<Player>();
            var random = new Random();

            for (var i = 0; i < count; i++)
            {
                var radius = random.Next(BubbleConfig.MinNpcInitialRadius, BubbleConfig.MaxNpcInitialRadius);
                var posX = random.Next(0, CanvasConfig.WorldWidth);
                var posY = random.Next(0, CanvasConfig.WorldHeight);
                var position = new Point2D(posX, posY);

                npcs.Add(new Player { Bubble = new Bubble { Radius = radius, Position = position }, IsNpc = true });
            }

            return npcs;
        }
    }
}
