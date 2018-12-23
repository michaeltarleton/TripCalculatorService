using System;
using Microsoft.Extensions.Configuration;

namespace ElasticDataAccess
{
    internal class ElasticDataAccessClient
    {
        private readonly IConfiguration _configuration;
        private readonly ElasticClient _client;
        public ElasticDataAccessClient(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public ElastiClient Build()
        {
            var node = new Uri(this._configuration.get);
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);
        }
    }
}