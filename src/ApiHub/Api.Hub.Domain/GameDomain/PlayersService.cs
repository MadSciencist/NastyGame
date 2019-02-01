using Api.Hub.Domain.DTOs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Api.Hub.Domain.GameDomain
{
    public class PlayersService : IPlayersService
    {
        private readonly ILogger<PlayersService> _logger;
        private readonly IList<Player> _players;

        public PlayersService(ILogger<PlayersService> logger)
        {
            _logger = logger;
            _players = new List<Player>();
        }

        public int GetCount() => _players.Count;
        public IList<EnemyBubblesDto> GetPlayers() => _players.Select(player => new EnemyBubblesDto(player)).ToList();

        public void AddPlayer(string connectionId, bool isAuthenticated)
        {
            var player = _players.FirstOrDefault(p => p.ConnectionId == connectionId);

            if (player != null)
            {
                _logger.LogInformation($"Player: {connectionId}, isAuth: {isAuthenticated} already added. Removing him.");
                RemovePlayer(connectionId);
            }

            var defaultBubble = new Bubble { Position = new Point2D(200, 200), Radius = 20 };
            _players.Add(new Player { ConnectionId = connectionId, Bubble = defaultBubble, IsAuthenticated = isAuthenticated });

            _logger.LogInformation($"Added player: {connectionId}, isAuth: {isAuthenticated}.");
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

            _players.Remove(player);
            _logger.LogInformation($"Removed player: {connectionId} name: {player?.Name}");
        }

        public void Update(string connectionId, BubbleDto bubbleDto)
        {
            var player = _players.FirstOrDefault(p => p.ConnectionId == connectionId);

            if (player == null)
            {
                _logger.LogInformation($"Player: {connectionId} doesnt exist.");
            }
            else
            {
                player.Bubble = new Bubble(bubbleDto);
            }
        }
    }
}
