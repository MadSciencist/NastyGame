using Api.Hub.Domain.Services;
using Api.Hub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.GameDomain;

namespace Api.Hub.Services
{
    public class PlayerNotifierTask : NotifierTaskBase, INotifierTask
    {
        private readonly IHubContext<GameHub> _gameHub;
        private readonly IGameplay _gameplay;
        private readonly IPlayersService _playersService;
        private readonly ILogger<PlayerNotifierTask> _logger;

        public PlayerNotifierTask(IHubContext<GameHub> gameHub, IGameplay gameplay, IPlayersService players, ILogger<PlayerNotifierTask> logger) : base(logger)
        {
            _gameHub = gameHub;
            _gameplay = gameplay;
            _playersService = players;
            _logger = logger;
            _playersService.PlayerRemoved += PlayersServiceOnPlayerRemoved;
        }

        private async void PlayersServiceOnPlayerRemoved(object sender, string e)
        {
            await _gameHub.Clients.Client(e).SendAsync("Lost", "lolololo");
        }

        protected override async void Execute()
        {
            try
            {
                Thread.Sleep(20);

                if (_playersService.GetCount() > 0)
                {
                    _gameplay.UpdateGameplay();
                    var players = _playersService.GetPlayers().Select(player => new EnemyBubblesDto(player)).ToList();
                    await _gameHub.Clients.All.SendAsync("UpdateEnemies", players);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while exeuting task: UpdateEnemies.");
            }
        }
    }
}
