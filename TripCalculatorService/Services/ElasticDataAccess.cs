using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using TripCalculatorService;

namespace TripCalculatorService
{
    internal static class DataAccessExtentionMethods
    {
        internal static TConfig ConfigureStronglyTypedAppSettings<TConfig>(this IServiceCollection services, IConfiguration configuration, TConfig config) where TConfig : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (config == null) throw new ArgumentNullException(nameof(config));

            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }
        internal static void ConfigureElasticSearch(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IElasticClient>((s) =>
            {
                var settings = s.GetService<AppSettings>();

                if (settings == null) throw new ArgumentNullException(nameof(settings));

                var node = new Uri(settings.ElasticConfig.Host);
                var config = new ConnectionSettings(node);
                var client = new ElasticClient(config);

                return client;
            });
        }
    }
}