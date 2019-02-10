using Api.Common.Messaging.Abstractions;
using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.GameDomain;
using Api.Hub.Domain.Services;
using Api.Hub.Events;
using Api.Hub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using Api.Hub.Domain.EventArguments;

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
            _playersService.PlayerJoined += PlayersServiceOnPlayerJoined;
            _playersService.PlayerRemoved += PlayersServiceOnPlayerRemoved;
            _playersService.PlayerScored += PlayersServiceOnPlayerScored;
        }

        private void PlayersServiceOnPlayerJoined(object sender, Player ev)
        {
            if (ev.IsAuthenticated)
            {
                var @event = new PlayerStartedNewGameEvent(ev.GlobalId, ev.JoinedTime);
                _eventBus.Publish(@event);
            }
        }

        private async void PlayersServiceOnPlayerScored(object sender, PlayerScoredEventArgs ev)
        {
            // publish message to RabbitMQ
            if (ev.Murderer.IsAuthenticated)
            {
                var @event = new UpdateUserKillsEvent(ev.Murderer.GlobalId, ev.VictimId, ev.Murderer.Victims.LastOrDefault());
                _eventBus.Publish(@event);
            }

            // publish message to SignalR client
            await _gameHub.Clients.Client(ev.Murderer.ConnectionId).SendAsync("Scored", new PlayerDto(ev.Murderer));
        }

        private async void PlayersServiceOnPlayerRemoved(object sender, Player ev)
        {
            if (ev.IsAuthenticated)
            {
                var @event = new UpdateUserDeathsEvent(ev.GlobalId, ev.KilledById, ev.KilledBy, DateTime.Now);
                _eventBus.Publish(@event);
            }

            await _gameHub.Clients.Client(ev.ConnectionId).SendAsync("Lost", new PlayerDto(ev));
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
