using Api.Hub.Domain.GameDomain;
using System.Collections.Generic;

namespace Api.Hub.Domain.Services
{
    public interface INpcService
    {
        IEnumerable<Player> GetDefaultCountOfNpcs();
        IEnumerable<Player> GenerateNpcs(int count);
        int GetCountOfNeededNpcs(IEnumerable<Player> players);
    }
}