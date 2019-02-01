using Api.Hub.Domain.GameDomain;
using Api.Hub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Api.Hub.Services
{
    public class NpcSpawnerTask : NotifierTaskBase, INpcSpawnerTask
    {
        private readonly IHubContext<GameHub> _gameHub;
        private readonly IPlayersService _playersService;
        private readonly ILogger<NpcSpawnerTask> _logger;

        public NpcSpawnerTask(IHubContext<GameHub> gameHub, IPlayersService playersService, ILogger<NpcSpawnerTask> logger) : base(logger)
        {
            _gameHub = gameHub;
            _playersService = playersService;
            _logger = logger;
        }

        protected override async void Execute()
        {
            try
            {
                Thread.Sleep(500);

                if (_playersService.GetCount() > 0)
                {
                    await _gameHub.Clients.All.SendAsync("SpawnNpcs", "aaa");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while exeuting task: SpawnNpcs.");
            }
        }
    }
}
