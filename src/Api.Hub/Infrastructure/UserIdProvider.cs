using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Api.Hub.Infrastructure
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            // do modelu dodac conn ID, i jesli user == null to brac wtedy conn ID, a client przy starcie jesli bedzie bez tokenu wysle swoj nick
            // dodac tez pole czy isAuth, gdzie bedziemy sprawdzac czy updatowac baze
            var claimedName = connection.User?.FindFirst(ClaimTypes.Name)?.Value;
            return claimedName ?? connection.ConnectionId;
        }
    }
}
