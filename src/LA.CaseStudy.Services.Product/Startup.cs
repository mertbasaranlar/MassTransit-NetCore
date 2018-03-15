using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LA.CaseStudy.Common.MassTransit;
using LA.CaseStudy.Common.MongoDb;
using LA.CaseStudy.Services.Product.Domain.Repositories;
using LA.CaseStudy.Services.Product.MassTransit;
using LA.CaseStudy.Services.Product.Repositories;
using LA.CaseStudy.Services.Product.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LA.CaseStudy.Services.Product
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RabbitMqOptions>(Configuration.GetSection("rabbitmq"));
            services.Configure<MongoDbOptions>(Configuration.GetSection("mongodb"));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddMvc();
            services.AddMongoDb(Configuration);
            services.AddMassTransit(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
