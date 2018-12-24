using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;
using TripCalculatorService.Configuration;

namespace TripCalculatorService {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices (IServiceCollection services) {
            services.AddOptions ();

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);

            services.ConfigureStronglyTypedAppSettings (this.Configuration);

            services.ConfigureElasticSearch ();

            return services.BuildServiceProvider ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }

            app.SeedElasticsearch ();

            app.UseHttpsRedirection ();
            app.UseMvc ();
        }
    }
}