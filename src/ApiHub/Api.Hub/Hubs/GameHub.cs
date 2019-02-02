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
        private readonly INotifierTask _playersNotifier;
        private readonly ILogger<GameHub> _logger;

        public GameHub(IPlayersService players, INotifierTask playersNotifier, ILogger<GameHub> logger)
        {
            _playersService = players;
            _playersNotifier = playersNotifier;
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            var isAuthenticated = Context.UserIdentifier != null;
            var connectionid = Context.ConnectionId;
            _playersService.AddPlayer(connectionid, isAuthenticated);
            _logger.LogInformation($"New connection: {connectionid} isAuth: {isAuthenticated}");

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

        public GameConfigDto RegisterName(string name)
        {
            _playersService.SetName(Context.ConnectionId, name);
            StartCyclicTasks();
            return new GameConfigDto(Context.ConnectionId, name);
        }

        public void Update(BubbleDto bubble) => _playersService.Update(Context.ConnectionId, bubble);
        
        private void StartCyclicTasks()
        {
            _logger.LogInformation($"Starting cyclic tasks");

            if (_playersService.GetCount() > 0)
            {
                if (_playersNotifier.State == NotifierState.Stopped)
                {
                    _playersNotifier.Start();
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
            }
        }
    }
}