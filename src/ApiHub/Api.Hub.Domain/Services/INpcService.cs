using Api.Hub.Domain.DTOs;
using System;
using System.Collections.Generic;
using Api.Hub.Domain.GameDomain;

namespace Api.Hub.Domain.Services
{
    public interface INpcService
    {
        IList<NpcBubble> GetNpcs();
        void KillNpc(Guid guid);
    }
}