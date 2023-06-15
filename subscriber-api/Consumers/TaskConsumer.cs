using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace subscriber_api.Consumers
{
    public class TaskConsumer : BackgroundService
    {
        private readonly string _queueName = "task";
        private int _attemptRetry = 0;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", AutomaticRecoveryEnabled = true };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, args) =>
            {
                try
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Mensagem processada: {message} na fila {_queueName}");
                    channel.BasicAck(args.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _attemptRetry++;

                    if (_attemptRetry > 3)
                    {
                        Console.WriteLine($"Mensagem rejeitada após 3 tentativas falhas");
                        channel.BasicReject(args.DeliveryTag, requeue: false);
                        _attemptRetry = 0;
                    }
                    else
                    {
                        Console.WriteLine($"Erro durante o processamento: {ex.Message}");
                        channel.BasicNack(args.DeliveryTag, false, requeue: true);
                    }
                }
            };

            channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}