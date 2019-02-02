using System;
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
            foreach (var player in players.Where(p => p.IsNpc == false).ToList())
            {
                foreach (var opponent in players.ToList())
                {
                    if (player == opponent)
                    {
                        continue; // skip myself
                    }

                    if (player.TryKill(opponent) && !player.IsDown)
                    {
                        opponent.IsDown = true;

                        if (player is Player killer)
                        {
                            _playersService.UpdateStats(killer, opponent);
                        }

                        _playersService.KillPlayer(opponent);
                    }
                }
            }
        }
    }
}
