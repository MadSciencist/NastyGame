using Api.Hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Api.Hub.Models.DTOs;

namespace Api.Hub.BusinessLogic
{
    public class Players : IPlayers
    {
        private IList<Bubble> _bubbles;

        public Players()
        {
            _bubbles = new List<Bubble>();
        }

        public int GetCount() => _bubbles.Count;
        public IEnumerable<Bubble> GetPlayers() => _bubbles;
    

        public void AddPlayer(string name)
        {
            var player = _bubbles.FirstOrDefault(p => p.Name == name);

            if (player != null)
            {
                Console.WriteLine("Player already in game");
                RemovePlayer(name);
            }

            _bubbles.Add(new Bubble { Name = name, Position = new Point2D(200, 200), Radius = 20});
        }

        public void RemovePlayer(string name)
        {
            var player = _bubbles.FirstOrDefault(p => p.Name == name);

            if (player == null)
            {
                Console.WriteLine("Player does not exist");
            }

            _bubbles.Remove(player);
        }

        public void Update(string name, BubbleDto bubbleDto)
        {
            var old = _bubbles.FirstOrDefault(p => p.Name == name);

            if (old == null)
            {
                Console.WriteLine("Player does not exist");
            }
            else
            {
                _bubbles.Remove(old);
                _bubbles.Add(new Bubble(name, bubbleDto));
            }
        }
    }
}
