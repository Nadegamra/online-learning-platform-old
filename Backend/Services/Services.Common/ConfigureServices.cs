using Infrastructure.EventBus.Generic;
using Infrastructure.EventBus.Generic.Subscriptions;
using Infrastructure.EventBus.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Services.Common
{
    public class ConfigureServices
    {
        public static WebApplicationBuilder AddEventBus(WebApplicationBuilder builder)
        {
            var eventBusSection = builder.Configuration.GetSection("EventBus");
            if (!eventBusSection.Exists())
            {
                return builder;
            }

            builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = eventBusSection["HostName"],
                    Port = int.Parse(eventBusSection["Port"]),
                    DispatchConsumersAsync = true,
                    UserName = eventBusSection["UserName"],
                    Password = eventBusSection["Password"],
                };

                var retryCount = eventBusSection.GetValue("RetryCount", 5);

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            builder.Services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = eventBusSection["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var retryCount = eventBusSection.GetValue("RetryCount", 5);

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubscriptionsManager, subscriptionClientName, retryCount);
            });

            return builder;
        }
    }
}
