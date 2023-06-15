using publisher_api.Services;
using RabbitMQ.Client;

namespace publisher_api.Extensions
{
    public static class RabbitMqConnectionExtensions
    {
        public static void ConfigureRabbitMqServices(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory>(provider =>
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                return factory;
            });

            services.AddSingleton<IConnection>(provider =>
            {
                var connectionFactory = provider.GetService<IConnectionFactory>();
                return connectionFactory.CreateConnection();
            });

            services.AddSingleton<IModel>(provider =>
            {
                var connection = provider.GetService<IConnection>();
                return connection.CreateModel();
            });

            services.AddSingleton<ITaskPublisher, TaskPublisher>();
        }
    }
}