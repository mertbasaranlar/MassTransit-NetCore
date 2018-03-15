using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LA.CaseStudy.Common.MongoDb
{
    public static class Extensions
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbOptions>(configuration.GetSection("mongodb"));

            services.AddSingleton<MongoClient>(c => 
            {
                var options = c.GetService<IOptions<MongoDbOptions>>();

                return new MongoClient(options.Value.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(c =>
            {
                var options = c.GetService<IOptions<MongoDbOptions>>();

                var client = c.GetService<MongoClient>();

                return client.GetDatabase(options.Value.Database);
            });
        }
    }
}