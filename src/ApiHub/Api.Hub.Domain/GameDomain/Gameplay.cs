using Api.Hub.Domain.Services;
using System.Linq;

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

            // ToList is necessary to create copy of the list,so we dont modify enumerable while iterating
            foreach (var player in players.ToList())
            {
                foreach (var opponent in players.ToList())
                {
                    if (player == opponent)
                    {
                        continue;
                    } // skip myself

                    if (player.Bubble.CanBeat(opponent.Bubble))
                    {
                        if (opponent.IsNpc)
                            _playersService.KillPlayer(opponent);
                    }
                    //if (opponent.Bubble.CanBeat(player.Bubble)) markedToKill.Add(player);
                }
            }
        }
    }
}
