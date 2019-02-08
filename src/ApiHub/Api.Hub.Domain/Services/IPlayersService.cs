using System;
using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.GameDomain;
using System.Collections.Generic;

namespace Api.Hub.Domain.Services
{
    public interface IPlayersService
    {
        void AddPlayer(string connectionId, bool isAuthenticated, int globalId, string globalName);
        void SetName(string connectionId, string name);
        void RemovePlayer(string connectionId);
        IList<PlayerBase> GetPlayers();
        int GetCount();
        void Update(string connectionId, BubbleDto bubbleDto);
        void KillPlayer(PlayerBase player);
        void UpdateStats(PlayerBase killer, PlayerBase victim);
        event EventHandler<Player> PlayerJoined;
        event EventHandler<Player> PlayerRemoved;
        event EventHandler<Player> PlayerScored;
    }
}