using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using TripCalculatorService;
using TripCalculatorService.Entities;

namespace TripCalculatorService.Configuration
{
    internal static class ConfigurationExtensions
    {
        internal static void ConfigureStronglyTypedAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            AppSettings settings = new AppSettings();
            configuration.Bind(settings);
            services.AddSingleton<AppSettings> (settings);
        }

        internal static void ConfigureElasticSearch(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IElasticClient> ((s) => {
                AppSettings settings = s.GetService<AppSettings>();

                if (settings == null) throw new ArgumentNullException(nameof(settings));

                ElasticConfig esConfig = settings.ElasticConfig;
                if (esConfig == null) throw new ArgumentNullException("The Elasticsearch configuration was not found!");

                string esHost = settings.ElasticConfig.Host;
                if (string.IsNullOrWhiteSpace(esHost)) throw new ArgumentNullException("The Elasticsearch host was not found!");

                string esDefaultIndex = settings.ElasticConfig.DefaultIndex;
                if (string.IsNullOrWhiteSpace(esDefaultIndex)) throw new ArgumentNullException("The Elasticsearch default index was not found!");

                Uri node = new Uri(esHost);
                ConnectionSettings config = new ConnectionSettings(node).DefaultIndex(esDefaultIndex);
                ElasticClient client      = new ElasticClient(config);

                return client;
            });
        }

        internal static void SeedElasticsearch(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            IElasticClient client   = app.ApplicationServices.GetService<IElasticClient>();
            AppSettings    settings = app.ApplicationServices.GetService<AppSettings>();
            string         index    = settings != null &&
                                      settings.ElasticConfig != null &&
                                      string.IsNullOrWhiteSpace(settings.ElasticConfig.DefaultIndex) ?
                                      settings.ElasticConfig.DefaultIndex :
                                      "friends";

            var observable = Observable.Create<bool> (async o => {
                var elasticHealth = await client.CatHealthAsync();
                if (elasticHealth.IsValid)
                {
                    Console.WriteLine("Elasticsearch is UP!");
                    o.OnNext(true);
                    o.OnCompleted();
                }
                else
                {
                    Console.WriteLine("Elasticsearch is down!");
                    o.OnError(new Exception("Elasticsearch is down"));
                }
            });

            var observer = Observer.Create<bool> (isElasticsearchUp => {
                // Remove then create the index
                client.DeleteIndex(index);
                client.CreateIndex(index, c => c.Mappings(ms => ms.Map<Friend> (m => m.AutoMap())));

                // Bulk insert random friends; bulk for performance
                client.Bulk((s) => {
                    return s.IndexMany(Friend.BuildRandomFriends(10));
                });
            });

            observable.DelaySubscription(TimeSpan.FromSeconds(1)).Retry(60).Subscribe(observer);
        }
    }
}
