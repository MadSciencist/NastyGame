using Api.Hub.Domain.Services;
using System.Linq;

namespace Api.Hub.Domain.GameDomain
{
    public class Gameplay : IGameplay
    {
        private readonly IPlayersService _playersService;

        public Gameplay(IPlayersService playersService)
        {
            _playersService = playersService;
        }

        public void UpdateGameplay()
        {
            var players = _playersService.GetPlayers();

            // ToList is necessary to create copy of the list,so we dont modify enumerable while iterating
            // Select only players (not NPC) to reduce computation conplexity from O(x^2) (its actually the same, but in worst case, in best one its nearly O(x))
            foreach (var player in players.Where(p => p.IsNpc == false).ToList())
            {
                foreach (var opponent in players.ToList())
                {
                    if (player == opponent)
                    {
                        continue; // skip myself
                    }

                    // Check if current player can kill other, which is not marked as dead
                    // Marking IsDown is needed, so in some cases we don't try to kill already killed player (which is disconnected)
                    if (player.TryKill(opponent) && !player.IsDown)
                    {
                        opponent.IsDown = true;

                        if (player is Player killer)
                        {
                            _playersService.UpdateStats(killer, opponent);
                        }

                        _playersService.KillPlayer(opponent);
                    }
                    // now lets check opposite - if NPC or opponent can kill player
                    else if (opponent.TryKill(player) && !opponent.IsDown)
                    {
                        player.IsDown = true;

                        if (opponent is Player killer)
                        {
                            _playersService.UpdateStats(killer, player);
                        }

                        _playersService.KillPlayer(player);
                    }
                }
            }
        }
    }
}
