using Api.Hub.Domain.Services;
using Api.Hub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using Api.Common.Messaging.Abstractions;
using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.GameDomain;
using Api.Hub.Events;

namespace Api.Hub.Services
{
    public class PlayerNotifierTask : NotifierTaskBase, INotifierTask
    {
        private readonly IHubContext<GameHub> _gameHub;
        private readonly IEventBus _eventBus;
        private readonly IGameplay _gameplay;
        private readonly IPlayersService _playersService;
        private readonly ILogger<PlayerNotifierTask> _logger;

        public PlayerNotifierTask(IHubContext<GameHub> gameHub, IEventBus eventBus,  IGameplay gameplay, IPlayersService players, ILogger<PlayerNotifierTask> logger) : base(logger)
        {
            _gameHub = gameHub;
            _eventBus = eventBus;
            _gameplay = gameplay;
            _playersService = players;
            _logger = logger;
            _playersService.PlayerRemoved += PlayersServiceOnPlayerRemoved;
            _playersService.PlayerScored += PlayersServiceOnPlayerScored;
        }

        private async void PlayersServiceOnPlayerScored(object sender, Player player)
        {
            // publish message to RabbitMQ
            if (player.IsAuthenticated)
            {
                var @event = new UpdateUserKillsEvent(player.GlobalId, player.Victims.LastOrDefault());
                _eventBus.Publish(@event);
            }

            // publish message to SignalR client
            await _gameHub.Clients.Client(player.ConnectionId).SendAsync("Scored", new PlayerDto(player));
        }

        private async void PlayersServiceOnPlayerRemoved(object sender, Player player)
        {
            if (player.IsAuthenticated)
            {
                var @event =
                    new UpdateUserDeathsEvent(player.GlobalId, player.KilledBy, DateTime.Now - player.JoinedTime);
                _eventBus.Publish(@event);
            }

            await _gameHub.Clients.Client(player.ConnectionId).SendAsync("Lost", new PlayerDto(player));
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
