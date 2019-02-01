using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.Services;
using Api.Hub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;

namespace Api.Hub.Services
{
    public class NpcSpawnerTask : NotifierTaskBase, INpcSpawnerTask
    {
        private readonly IHubContext<GameHub> _gameHub;
        private readonly IPlayersService _playersService;
        private readonly INpcService _npcService;
        private readonly ILogger<NpcSpawnerTask> _logger;

        public NpcSpawnerTask(IHubContext<GameHub> gameHub, IPlayersService playersService, INpcService npcService, ILogger<NpcSpawnerTask> logger) : base(logger)
        {
            _gameHub = gameHub;
            _playersService = playersService;
            _npcService = npcService;
            _logger = logger;
        }

        protected override async void Execute()
        {
            try
            {
                Thread.Sleep(200);

                if (_playersService.GetCount() > 0)
                {
                    var npcs = _npcService.GetNpcs().Select(bubble => new NpcBubbleDto(bubble)).ToList();
                    await _gameHub.Clients.All.SendAsync("SpawnNpcs", npcs);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception while exeuting task: SpawnNpcs.");
            }
        }
    }
}
