using Api.Hub.Domain.DTOs;
using System.Collections.Generic;

namespace Api.Hub.Domain.GameDomain
{
    public interface IPlayersService
    {
        void AddPlayer(string connectionId, bool isAuthenticated);
        void SetName(string connectionId, string name);
        void RemovePlayer(string connectionId);
        IList<EnemyBubblesDto> GetPlayers();
        int GetCount();
        void Update(string connectionId, BubbleDto bubbleDto);
    }
}