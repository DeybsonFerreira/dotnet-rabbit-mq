using subscriber_api.Consumers;

namespace subscriber_api.Extensions
{
    public static class RabbitMqConnectionExtensions
    {
        /// <summary>
        /// Configurar Injeção de dependêciar
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRabbitMqServices(this IServiceCollection services)
        {
            // ::NÃO UTILIZADO MAIS::
            //    services.AddSingleton<IConnectionFactory>(provider =>
            //    {
            //        var factory = new ConnectionFactory()
            //        {
            //            HostName = "localhost",
            //            AutomaticRecoveryEnabled = true
            //        };
            //        return factory;
            //    });

            //    services.AddSingleton<IConnection>(provider =>
            //    {
            //        var connectionFactory = provider.GetService<IConnectionFactory>();
            //        return connectionFactory.CreateConnection();
            //    });

            //    services.AddScoped<IModel>(provider =>
            //    {
            //        var connection = provider.GetService<IConnection>();
            //        var channel = connection.CreateModel();
            //        channel.ModelShutdown += ((sender, args) =>
            //        {
            //            channel = connection.CreateModel();
            //        });
            //        return channel;
            //    });


            //services.AddSingleton<ITaskConsumer, TaskConsumer>();
            //services.AddSingleton<ITaskErrorConsumer, TaskErrorConsumer>();
            services.AddHostedService<TaskConsumer>();
            services.AddHostedService<TaskErrorConsumer>();
        }

        /// <summary>
        /// Executar Subscribers :: NÃO UTILIZADO MAIS ::
        /// </summary>
        public static void ExecuteConsumers(this IApplicationBuilder app)
        {
            //var taskConsumer = app.ApplicationServices.GetService<ITaskConsumer>();
            //var taskErrorConsumer = app.ApplicationServices.GetService<ITaskErrorConsumer>();

            //taskConsumer.StartConsuming();
            //taskErrorConsumer.StartConsuming();
        }
    }
}