using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Integrations
{
    public class IntegrationManager
    {
        public IntegrationManager()
        {
            InitializeListener();
        }

        private void InitializeListener()
        {
            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("ApplicationIntegration", "topic");
                var queueName = channel.QueueDeclare().QueueName;

                channel.QueueBind(queue: queueName,
                    exchange: "ApplicationIntegration",
                    routingKey: "Finance.#");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += OnMessageReceived;
                channel.BasicConsume(queue: queueName,
                    noAck: true,
                    consumer: consumer);
            }
        }

        private static void OnMessageReceived(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            throw new NotImplementedException();
        }
    }
}

//consumer.Received += (model, ea) =>
//                {
//                    var body = ea.Body;
//var message = Encoding.UTF8.GetString(body);
//var routingKey = ea.RoutingKey;
//Console.WriteLine(" [x] Received '{0}':'{1}'",
//                                      routingKey,
//                                      message);
//                };