using System;
using LA.CaseStudy.Common.Commands;
using LA.CaseStudy.Common.MassTransit;
using MassTransit;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace LA.CaseStudy.Common.Service
{
    public class ServiceHost
    {
        private readonly IWebHost _webHost;

         public ServiceHost(IWebHost webHost)
         {
             _webHost = webHost;
         }

         public void Run() => _webHost.Run();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;

            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder()
                .UseConfiguration(config)
                .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        } 

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;

            private IBusControl _bus;

            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public BusBuilder UseMassTransit()
            {
                _bus = (IBusControl)_webHost.Services.GetService(typeof(IBusControl));

                return new BusBuilder(_webHost, _bus);
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;

            private IBusControl _bus;

            public BusBuilder(IWebHost webHost, IBusControl bus)
            {
                _webHost = webHost;
                _bus = bus;
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }
    }
}