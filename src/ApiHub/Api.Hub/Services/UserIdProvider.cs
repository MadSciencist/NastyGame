using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Api.Hub.Services
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var claimedName = connection.User?.FindFirst(ClaimTypes.Name)?.Value;
            return claimedName; // if its null, then we know that user is playing as anonymous
        }
    }
}
