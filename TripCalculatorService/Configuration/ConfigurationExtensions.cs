using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nest;
using System;
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
            services.AddSingleton <AppSettings> (settings);
        }

        internal static void ConfigureElasticSearch(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped <IElasticClient> ((s) => {
                var settings = s.GetService <AppSettings> ();

                if (settings == null) throw new ArgumentNullException(nameof(settings));

                ElasticConfig esConfig = settings.ElasticConfig;
                if (esConfig == null) throw new ArgumentNullException("The Elasticsearch configuration was not found!");

                string esHost = settings.ElasticConfig.Host;
                if (string.IsNullOrWhiteSpace(esHost)) throw new ArgumentNullException("The Elasticsearch host was not found!");

                var node   = new Uri(esHost);
                var config = new ConnectionSettings(node);
                var client = new ElasticClient(config);

                return(client);
            });
        }

        internal static void SeedElasticsearch(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            IElasticClient client = app.ApplicationServices.GetService <IElasticClient> ();
            string         index  = "friends";

            client.DeleteIndex(index);

            client.CreateIndex(index, c => c.Mappings(ms => ms
                    .Map <Friend> (m => m
                        .AutoMap()
                        .Properties(ps => ps
                            .Text(s => s
                                .Name(e => e.Id)
                                .Fields(fs => fs
                                    .Keyword(ss => ss
                                        .Name("_id")
                                    )
                                )
                            )
                        )
                    )
                ));

            client.CreateDocument(Friend.BuildRandomFriend());
        }
    }
}
