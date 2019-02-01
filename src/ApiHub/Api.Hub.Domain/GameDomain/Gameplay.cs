using Api.Hub.Domain.Services;
using System.Linq;

namespace Api.Hub.Domain.GameDomain
{
    public class Gameplay
    {
        private readonly IPlayersService _playersService;
        private readonly INpcService _npcService;

        public Gameplay(IPlayersService playersService, INpcService npcService)
        {
            _playersService = playersService;
            _npcService = npcService;
        }

        void UpdateGameplay()
        {
            // tests so far
            var npc = _npcService.GetNpcs().First();
            var player = _playersService.GetPlayers().First();

            var can = npc.CanBeat(player);

            var other = player.CanBeat(npc);
        }
    }
}
