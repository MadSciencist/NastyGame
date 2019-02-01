using System.Collections.Generic;
using Api.Hub.Models;
using Api.Hub.Models.DTOs;

namespace Api.Hub.BusinessLogic
{
    public interface IPlayers
    {
        void AddPlayer(string name);
        void RemovePlayer(string name);
        IEnumerable<Bubble> GetPlayers();
        int GetCount();
        void Update(string name, BubbleDto bubbleDto);
    }
}