using subscriber_api.Consumers;

namespace subscriber_api.Extensions
{
    public static class RabbitMqConnectionExtensions
    {
        /// <summary>
        /// Configurar Inje��o de depend�ciar
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureRabbitMqServices(this IServiceCollection services)
        {
            // ::N�O UTILIZADO MAIS::
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
        /// Executar Subscribers :: N�O UTILIZADO MAIS ::
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