using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Hub.Services
{
    public class NotifierTaskBase
    {
        private readonly ILogger<NotifierTaskBase> _logger;
        private CancellationTokenSource _source;
        public NotifierState State { get; private set; } = NotifierState.Stopped;

        public NotifierTaskBase(ILogger<NotifierTaskBase> logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            State = NotifierState.Started;
            _source = new CancellationTokenSource();
            Task.Factory.StartNew(TaskRun, _source.Token).ContinueWith(ExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);
            _logger.LogInformation("Task is starting");
        }

        public void Stop()
        {
            State = NotifierState.Stopped;
            _source.Cancel();
            _logger.LogInformation("Task is stopping");
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

                    Execute();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Task cancellation request");
            }
        }

        protected virtual async void Execute()
        {
            throw new NotImplementedException();
        }


        protected virtual void ExceptionHandler(Task task)
        {
            _logger.LogError($"Task got exception: {task.Exception}. Stopping this task.");
        }
    }
}