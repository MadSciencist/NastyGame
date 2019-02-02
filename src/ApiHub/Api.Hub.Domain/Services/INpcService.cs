using Api.Hub.Domain.GameDomain;
using System.Collections.Generic;

namespace Api.Hub.Domain.Services
{
    public interface INpcService
    {
        IEnumerable<NpcPlayer> GetDefaultCountOfNpcs();
        IEnumerable<NpcPlayer> GenerateNpcs(int count);
        int GetCountOfNeededNpcs(IEnumerable<PlayerBase> players);
    }
}