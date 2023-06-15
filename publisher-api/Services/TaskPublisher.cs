using RabbitMQ.Client;

namespace publisher_api.Services
{
    public class TaskPublisher : RabbitMQExtensions, ITaskPublisher
    {
        public string _queueName = "task";

        public TaskPublisher(IModel rabbitMqChannel) : base(rabbitMqChannel)
        {
            rabbitMqChannel.QueueDeclare(_queueName, true, false, false, null);
        }

        public void PublishMessage(string text)
        {
            bool publised = base.PublishMessage(_queueName, text);
            if (!publised)
                throw new ArgumentException($"Falha ao publicar :{text} na fila{_queueName}");
        }

    }
}