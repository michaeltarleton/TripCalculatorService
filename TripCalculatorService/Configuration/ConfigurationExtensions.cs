using System;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using TripCalculatorService;
using TripCalculatorService.Entities;

namespace TripCalculatorService.Configuration {
    internal static class ConfigurationExtensions {

        internal static TConfig ConfigureStronglyTypedAppSettings<TConfig> (this IServiceCollection services, IConfiguration configuration, TConfig config) where TConfig : class {
            if (services == null) throw new ArgumentNullException (nameof (services));
            if (configuration == null) throw new ArgumentNullException (nameof (configuration));
            if (config == null) throw new ArgumentNullException (nameof (config));

            configuration.Bind (config);
            services.AddSingleton (config);
            return config;
        }

        internal static void ConfigureElasticSearch (this IServiceCollection services) {
            if (services == null) throw new ArgumentNullException (nameof (services));

            services.AddScoped<IElasticClient> ((s) => {
                var settings = s.GetService<AppSettings> ();

                if (settings == null) throw new ArgumentNullException (nameof (settings));

                // TODO: Make this production ready
                // var node = new Uri (settings.ElasticConfig.Host);
                var config = new ConnectionSettings (new InMemoryConnection ()); // new ConnectionSettings (node);
                var client = new ElasticClient (config);

                return client;
            });
        }

        internal static void SeedElasticsearch (this IApplicationBuilder app) {
            if (app == null) throw new ArgumentNullException (nameof (app));

            IElasticClient client = app.ApplicationServices.GetService<IElasticClient> ();

            client.CreateIndexAsync ("friends", c => c.Mappings (ms => ms
                .Map<Friend> (m => m
                    .AutoMap ()
                    .Properties (ps => ps
                        .Text (s => s
                            .Name (e => e.Id)
                            .Fields (fs => fs
                                .Keyword (ss => ss
                                    .Name ("_id")
                                )
                            )
                        )
                    )
                )
            ));
        }
    }
}