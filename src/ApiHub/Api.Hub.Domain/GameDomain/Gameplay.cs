using Api.Hub.Domain.Services;
using System.Collections.Generic;

namespace Api.Hub.Domain.GameDomain
{
    public interface IGameplay
    {
        void UpdateGameplay();
    }

    public class Gameplay : IGameplay
    {
        private readonly IPlayersService _playersService;
        private readonly INpcService _npcService;

        public Gameplay(IPlayersService playersService, INpcService npcService)
        {
            _playersService = playersService;
            _npcService = npcService;
        }

        public void UpdateGameplay()
        {
            var players = _playersService.GetPlayers();
            var npcs = _npcService.GetNpcs();

            var markedToKill = new List<NpcBubble>();

            foreach (var player in players)
            {
                foreach (var npc in npcs)
                {
                    if (player.Bubble.CanBeat(npc.Bubble))
                    {
                        markedToKill.Add(npc);
                    }
                }
            }

            foreach (var kill in markedToKill)
            {
                _npcService.KillNpc(kill);
            }
        }
    }
}
