using RabbitMQ.Client;
using System;

namespace Api.Common.Messaging.RabbitMQ
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
