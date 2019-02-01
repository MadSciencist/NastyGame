using Api.Hub.BusinessLogic;
using Api.Hub.Models.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Api.Hub.Infrastructure;

namespace Api.Hub.Hubs
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IPlayers _players;
        private readonly IPlayersNotifierTask _playersNotifier;

        public GameHub(IPlayers players, IPlayersNotifierTask playersNotifier)
        {
            _players = players;
            _playersNotifier = playersNotifier;
        }

        public override Task OnConnectedAsync()
        {
            
            var name = Context.UserIdentifier;
            Console.WriteLine($"Connected: {Context.UserIdentifier}");

            _players.AddPlayer(name);

            if (_players.GetCount() > 0)
            {
                if (_playersNotifier.State == PlayerNotifierState.Stopped)
                {
                    _playersNotifier.Start();
                }
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"Disconnected: {Context.UserIdentifier}");

            if (_players.GetCount() == 0)
            {
                if (_playersNotifier.State == PlayerNotifierState.Started)
                {
                    _playersNotifier.Stop();
                }
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task RegisterName(string name)
        {
            Console.WriteLine("Registering: " + name);
        }

        public async Task Update(BubbleDto bubble)
        {
            var name = Context.UserIdentifier;
            Console.WriteLine($"R: {bubble.Radius}  X: {bubble.Position.x}  Y: {bubble.Position.y}");

            _players.Update(name, bubble);
        }
    }
}