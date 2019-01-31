//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Security.Claims;

//namespace Api.Hub.Infrastructure
//{
//    public class UserIdProvider : IUserIdProvider
//    {
//        public string GetUserId(HubConnectionContext connection)
//        {
//            var principal = ClaimsPrincipal.Current;
//            string user =  connection.User?.FindFirst(ClaimTypes.Name)?.Value;
//            return user;
//        }
//    }
//}
