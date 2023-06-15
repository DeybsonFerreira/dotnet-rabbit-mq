using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace publisher_api.Services
{
    public class RabbitMQExtensions
    {
        private readonly IModel _rabbitMqChannel;

        public RabbitMQExtensions(IModel rabbitMqChannel)
        {
            _rabbitMqChannel = rabbitMqChannel;
        }

        public bool PublishMessage(string queueName, string _queueNameErro, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            try
            {
                _rabbitMqChannel.ConfirmSelect();
                _rabbitMqChannel.BasicAcks += HandleSuccessBasicAck;
                _rabbitMqChannel.BasicPublish("", queueName, null, body);
                return true;
            }
            catch (Exception ex)
            {
                _rabbitMqChannel.BasicNacks += HandleErrorNacks;
                _rabbitMqChannel.BasicPublish("", _queueNameErro, null, body);
                Console.WriteLine($"Erro: ${ex.Message}");
                return false;
            }
        }

        private void HandleErrorNacks(object sender, BasicNackEventArgs eventArgs)
        {
            Console.WriteLine($"Falha ao publicar mensagem: ${eventArgs.DeliveryTag} Mensagem redirecionada para a fila de erro");
        }

        private void HandleSuccessBasicAck(object sender, BasicAckEventArgs eventArgs)
        {
            Console.WriteLine("Mensagem publicada com sucesso: " + eventArgs.DeliveryTag);
        }
    }
}
