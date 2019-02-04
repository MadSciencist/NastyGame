using Api.Common.Messaging.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client.Events;

namespace Api.Common.Messaging.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        const string BrokerName = "MainEventBus";
        private readonly IEventBusSubscriptionManager _subsManager;
        private readonly ILogger<RabbitMQEventBus> _logger;
        private readonly ILifetimeScope _autofac;
        private IModel _consumerChannel;
        private readonly IRabbitMQConnection _persistentConnection;
        private string _queueName;

        public RabbitMQEventBus(IRabbitMQConnection persistentConnection, ILogger<RabbitMQEventBus> logger,
            ILifetimeScope autofac, IEventBusSubscriptionManager subsManager, string queueName = null)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new EventBusSubscriptionManager();
            _queueName = queueName;
            _consumerChannel = CreateConsumerChannel();
            _autofac = autofac;
        }


        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            var channel = _persistentConnection.CreateModel();
            channel.ExchangeDeclare(BrokerName, "direct");
            channel.QueueDeclare(_queueName, true, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body);

                await ProcessEvent(eventName, message);

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(_queueName, false, consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };

            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(BrokerName))
                {
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        var handler = scope.ResolveOptional(subscription.HandlerType);
                        if (handler == null) continue;
                        var eventType = _subsManager.GetEventTypeByName(eventName);
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType.GetMethod("Handle")
                            .Invoke(handler, new object[] { integrationEvent });
                    }
                }
            }
        }

        public void Publish(IntegrationEvent @event)
        {
            var eventName = @event.GetType().Name;
            var factory = new ConnectionFactory() { HostName = "" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: BrokerName,
                    type: "direct");
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: BrokerName,
                    routingKey: eventName,
                    basicProperties: null,
                    body: body);
            }
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }
                using (var channel = _persistentConnection.CreateModel())
                {
                    channel.QueueBind(queue: _queueName,
                        exchange: BrokerName,
                        routingKey: eventName);
                }
            }
            _subsManager.AddSubscription<T, TH>();
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _consumerChannel?.Dispose();

            _subsManager.Clear();
        }
    }
}
