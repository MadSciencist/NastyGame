using Api.Hub.Domain.GameDomain;
using Api.Hub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

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
                    var players = _playersService.GetPlayers();
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
