using Api.Hub.Domain.DTOs;
using Api.Hub.Domain.GameDomain;
using System.Collections.Generic;

namespace Api.Hub.Domain.Services
{
    public interface IPlayersService
    {
        void AddPlayer(string connectionId, bool isAuthenticated);
        void SetName(string connectionId, string name);
        void RemovePlayer(string connectionId);
        IList<Player> GetPlayers();
        int GetCount();
        void Update(string connectionId, BubbleDto bubbleDto);
        void KillPlayer(Player player);
    }
}