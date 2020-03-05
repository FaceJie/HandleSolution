using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedHelper.MqMessages
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMqMessages(this IServiceCollection services
           )
        {
            services.AddSingleton<IMqMessagePublisher, RebusRabbitMqPublisher>();
            return services;
        }
    }
}
