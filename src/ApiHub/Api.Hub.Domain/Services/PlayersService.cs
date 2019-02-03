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
        public event EventHandler<Player> PlayerRemoved;
        public event EventHandler<Player> PlayerScored;
        private readonly INpcService _npcService;
        private readonly ILogger<PlayersService> _logger;
        private readonly List<PlayerBase> _players;

        public PlayersService(INpcService npcService, ILogger<PlayersService> logger)
        {
            _npcService = npcService;
            _logger = logger;
            _players = new List<PlayerBase>();
            _players.AddRange(npcService.GetDefaultCountOfNpcs());
        }

        public int GetCount() => _players.Count;
        public IList<PlayerBase> GetPlayers() => _players;

        public void AddPlayer(string connectionId, bool isAuthenticated, int globalId, string globalName)
        {
            var player = _players.FirstOrDefault(p => p.ConnectionId == connectionId);

            if (player != null)
            {
                _logger.LogInformation($"Player: {connectionId}, isAuth: {isAuthenticated} already added. Removing him.");
                RemovePlayer(connectionId);
            }

            var defaultBubble = new Bubble { Position = new Point2D((double)CanvasConfig.WorldWidth / 2, (double)CanvasConfig.WorldHeight / 2), Radius = BubbleConfig.InitialPlayerRadius };

            _players.Add(new Player
            {
                ConnectionId = connectionId,
                IsAuthenticated = isAuthenticated,
                GlobalId = globalId,
                GlobalName = globalName,
                IsNpc = false,
                Bubble = defaultBubble,
                JoinedTime = DateTime.UtcNow,
                Score = 0,
                Victims = new List<string>()
            });

            _logger.LogInformation($"Added player: {connectionId}, isAuth: {isAuthenticated}.");
        }

        // TODO debug  this, if one player kills another it gets executed twice in some cases
        public void UpdateStats(PlayerBase killer, PlayerBase victim)
        {
            if (killer is Player murderer)
            {
                murderer.Score++;
                if (victim is Player vict)
                {
                    murderer.Victims.Add(vict.Name);
                    vict.KilledBy = murderer.Name;

                    // TODO create new event and update stats service here (if user isAuth)
                }

                // TODO Use this event to update Stats service (if user isAuth)
                PlayerScored?.Invoke(this, murderer);
            }
        }

        public void KillPlayer(PlayerBase player)
        {
            if (player is Player victim)
            {
                PlayerRemoved?.Invoke(this, victim); // TODO Use this event to update Stats service (if user isAuth) [check if its not redundant with above]
            }             

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
            var player = _players.ToList().FirstOrDefault(p => p.ConnectionId == connectionId);

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
