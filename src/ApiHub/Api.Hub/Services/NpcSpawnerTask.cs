using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.Services;
using Api.Hub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using Api.Hub.Domain.GameDomain;

namespace Api.Hub.Services
{
    public class NpcSpawnerTask : NotifierTaskBase, INpcSpawnerTask
    {
        private readonly IHubContext<GameHub> _gameHub;
        private readonly IPlayersService _playersService;
        private readonly INpcService _npcService;
        private readonly IGameplay _gameplay;
        private readonly ILogger<NpcSpawnerTask> _logger;

        public NpcSpawnerTask(IHubContext<GameHub> gameHub, IPlayersService playersService, INpcService npcService, IGameplay gameplay, ILogger<NpcSpawnerTask> logger) : base(logger)
        {
            _gameHub = gameHub;
            _playersService = playersService;
            _npcService = npcService;
            _gameplay = gameplay;
            _logger = logger;
        }

        protected override async void Execute()
        {
            try
            {
                Thread.Sleep(33);

                if (_playersService.GetCount() > 0)
                {
                    _gameplay.UpdateGameplay();
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
