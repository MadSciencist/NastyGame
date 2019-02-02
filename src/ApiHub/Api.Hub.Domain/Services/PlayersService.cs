using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.GameConfig;
using Api.Hub.Domain.GameDomain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Hub.Domain.Services
{
    public class PlayersService : IPlayersService
    {
        public event EventHandler<string> PlayerRemoved;
        private readonly INpcService _npcService;
        private readonly ILogger<PlayersService> _logger;
        private readonly List<Player> _players;

        public PlayersService(INpcService npcService, ILogger<PlayersService> logger)
        {
            _npcService = npcService;
            _logger = logger;
            _players = new List<Player>();
            _players.AddRange(npcService.GetDefaultCountOfNpcs());
        }

        public int GetCount() => _players.Count;
        public IList<Player> GetPlayers() => _players;

        public void AddPlayer(string connectionId, bool isAuthenticated)
        {
            var player = _players.FirstOrDefault(p => p.ConnectionId == connectionId);

            if (player != null)
            {
                _logger.LogInformation($"Player: {connectionId}, isAuth: {isAuthenticated} already added. Removing him.");
                RemovePlayer(connectionId);
            }

            var defaultBubble = new Bubble { Position = new Point2D((double)CanvasConfig.WorldWidth / 2, (double)CanvasConfig.WorldHeight / 2), Radius = BubbleConfig.InitialPlayerRadius };
            _players.Add(new Player { ConnectionId = connectionId, IsNpc = false, Bubble = defaultBubble, IsAuthenticated = isAuthenticated });

            _logger.LogInformation($"Added player: {connectionId}, isAuth: {isAuthenticated}.");
        }

        public void KillPlayer(Player player)
        {
            // update this client that he lost
            if (player.IsNpc == false)
                PlayerRemoved?.Invoke(this, player.ConnectionId);

            _players.Remove(player);
        }

        public void SetName(string connectionId, string name)
        {
            var player = _players.FirstOrDefault(p => p.ConnectionId == connectionId);

            if (player == null)
            {
                _logger.LogInformation($"Player: {connectionId} requesting to set name: {name} doesnt exist.");
            }
            else
            {
                player.Name = name;
                _logger.LogInformation($"Updated player: {connectionId} to name: {name}.");
            }
        }

        public void RemovePlayer(string connectionId)
        {
            var player = _players.FirstOrDefault(p => p.ConnectionId == connectionId);

            if (player == null)
            {
                _logger.LogInformation($"Player: {connectionId} doesnt exist.");
            }
            else
            {
                _players.Remove(player);
                _logger.LogInformation($"Removed player: {connectionId} name: {player?.Name}");
            }
        }

        public void Update(string connectionId, BubbleDto bubbleDto)
        {
            var player = _players.ToList().FirstOrDefault(p => p.ConnectionId == connectionId);

            if (player == null)
            {
                _logger.LogInformation($"Player: {connectionId} doesnt exist.");
            }
            else
            {
                player.Bubble.Position = bubbleDto.Position;
                var neededNpcs = _npcService.GetCountOfNeededNpcs(_players);
                _players.AddRange(_npcService.GenerateNpcs(neededNpcs));
            }
        }
    }
}
