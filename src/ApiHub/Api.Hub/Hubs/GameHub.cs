using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.Services;
using Api.Hub.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Api.Hub.Hubs
{
    public class GameHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IPlayersService _playersService;
        private readonly IPlayerNotifierTask _playersNotifier;
        private readonly INpcSpawnerTask _npcSpawner;
        private readonly ILogger<GameHub> _logger;

        public GameHub(IPlayersService players, IPlayerNotifierTask playersNotifier, INpcSpawnerTask npcSpawner, ILogger<GameHub> logger)
        {
            _playersService = players;
            _playersNotifier = playersNotifier;
            _npcSpawner = npcSpawner;
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            var isAuthenticated = Context.UserIdentifier != null;
            var connectionid = Context.ConnectionId;
            _playersService.AddPlayer(connectionid, isAuthenticated);

            _logger.LogInformation($"New connection: {connectionid} isAuth: {isAuthenticated}");

            StartCyclicTasks();

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionid = Context.ConnectionId;
            _playersService.RemovePlayer(connectionid);

            StopCyclicTasks();

            _logger.LogInformation($"Disconnecting: {connectionid}");

            return base.OnDisconnectedAsync(exception);
        }

        public void RegisterName(string name)
        {
            _playersService.SetName(Context.ConnectionId, name);
        }

        public void Update(BubbleDto bubble)
        {
            _playersService.Update(Context.ConnectionId, bubble);
        }

        private void StartCyclicTasks()
        {
            _logger.LogInformation($"Starting cyclic tasks");

            if (_playersService.GetCount() > 0)
            {
                if (_playersNotifier.State == NotifierState.Stopped)
                {
                    _playersNotifier.Start();
                }
                if (_npcSpawner.State == NotifierState.Stopped)
                {
                    _npcSpawner.Start();
                }
            }
        }

        private void StopCyclicTasks()
        {
            _logger.LogInformation($"Stopping cyclic tasks");

            if (_playersService.GetCount() == 0)
            {
                if (_playersNotifier.State == NotifierState.Started)
                {
                    _playersNotifier.Stop();
                }
                if (_npcSpawner.State == NotifierState.Started)
                {
                    _npcSpawner.Stop();
                }
            }
        }
    }
}