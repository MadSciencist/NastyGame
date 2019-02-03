using Microsoft.AspNetCore.SignalR;

namespace Api.Hub.Infrastructure
{
    public interface IHubTokenHandler
    {
        (bool isAuth, int id, string name) GetClaimValues(HubCallerContext context);
    }
}