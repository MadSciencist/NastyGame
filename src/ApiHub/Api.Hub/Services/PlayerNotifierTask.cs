using Api.Hub.Domain.Services;
using Api.Hub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using Api.Hub.Domain.DTOs;

namespace Api.Hub.Services
{
    public class PlayerNotifierTask : NotifierTaskBase, IPlayerNotifierTask
    {
        private readonly IHubContext<GameHub> _gameHub;
        private readonly IPlayersService _playersService;
        private readonly ILogger<PlayerNotifierTask> _logger;

        public PlayerNotifierTask(IHubContext<GameHub> gameHub, IPlayersService players, ILogger<PlayerNotifierTask> logger) : base(logger)
        {
            _gameHub = gameHub;
            _playersService = players;
            _logger = logger;
        }

        protected override async void Execute()
        {
            try
            {
                Thread.Sleep(100);

                if (_playersService.GetCount() > 0)
                {
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
