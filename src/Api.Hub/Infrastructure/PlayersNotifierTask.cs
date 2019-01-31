using Api.Hub.BusinessLogic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Hub.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hub.Infrastructure
{
    public class PlayersNotifierTask : IPlayersNotifierTask
    {
        public PlayerNotifierState State { get; private set; } = PlayerNotifierState.Stopped;

        private readonly IPlayers _players;
        private readonly IHubContext<GameHub> _gameHub;
        private Task _task;
        private CancellationTokenSource _source;

        public PlayersNotifierTask(IPlayers players, IHubContext<GameHub> gameHub)
        {
            _players = players;
            _gameHub = gameHub;
        }

        public void Start()
        {
            State = PlayerNotifierState.Started;
            _source = new CancellationTokenSource();
            _task = Task.Factory.StartNew(TaskRun, _source.Token)
                .ContinueWith(ExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);
        }

        public void Stop()
        {
            State = PlayerNotifierState.Stopped;
            _source.Cancel();
        }

        private void TaskRun()
        {
            try
            {
                while (true)
                {
                    if (_source.IsCancellationRequested)
                    {
                        _source.Token.ThrowIfCancellationRequested();
                    }

                    TrySendData();
                }
            }
            catch (OperationCanceledException)
            {

            }
        }

        private async void TrySendData()
        {
            try
            {
                Thread.Sleep(1000);

                if (_players.GetCount() > 0)
                {
                    await _gameHub.Clients.All.SendAsync("UpdateEnemies", _players.GetPlayers());
                }
            }
            catch (Exception e)
            {

            }

        }

        private void ExceptionHandler(Task task)
        {

        }
    }
}
