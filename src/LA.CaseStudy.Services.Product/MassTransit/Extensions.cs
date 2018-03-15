using System;
using System.Reflection;
using LA.CaseStudy.Common.Commands;
using LA.CaseStudy.Common.MassTransit;
using LA.CaseStudy.Services.Product.Consumers;
using LA.CaseStudy.Services.Product.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LA.CaseStudy.Services.Product.MassTransit
{
    public static class Extensions
    {
        public static void AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
            var section = configuration.GetSection("rabbitmq");
            section.Bind(options);

            var provider = services.BuildServiceProvider();
            var productService = provider.GetService<IProductService>();

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(options.Uri), hst =>
                {
                    hst.Username(options.Username);
                    hst.Password(options.Password);
                });

                cfg.ReceiveEndpoint(host, QueueHelper.GetQueueName<AddProductCommand>(), e => {
                    e.Consumer(() => new AddProductConsumer(productService));
                });
            });

            bus.Start();

            services.AddSingleton<IBusControl>(_ => bus);
        }
    }
}