using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace publisher_api.Services
{
    public class TaskPublisher : RabbitMQExtensions, ITaskPublisher
    {
        public string _queueName = "task";
        public string _queueNameErro;

        public TaskPublisher(IModel rabbitMqChannel) : base(rabbitMqChannel)
        {
            _queueNameErro = $"{_queueName}_error";
            rabbitMqChannel.QueueDeclare(_queueNameErro, true, false, false, null);
            rabbitMqChannel.QueueDeclare(_queueName, true, false, false, null);
            ConsumingError(rabbitMqChannel);
        }

        public void PublishMessage(string text)
        {
            base.PublishMessage(_queueName, _queueNameErro, text);
        }

        public void ConsumingError(IModel rabbitMqChannel)
        {
            rabbitMqChannel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(rabbitMqChannel);
            consumer.Received += (sender, args) =>
            {
                try
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"reprocessamento: {body}");

                    rabbitMqChannel.BasicAck(args.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    // Lidar com o erro durante o reprocessamento (opcional)
                    Console.WriteLine($"Erro durante o reprocessamento: {ex.Message}");
                    rabbitMqChannel.BasicNack(args.DeliveryTag, false, true);
                }
            };

            rabbitMqChannel.BasicConsume(_queueNameErro, false, consumer);
        }
    }
}