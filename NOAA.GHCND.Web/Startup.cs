using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NOAA.GHCND.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NOAA.GHCND.Parser;
using NOAA.GHCND.Adapters;
using NOAA.GHCND.Rules;

namespace NOAA.GHCND.Web
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
            services.AddControllers();

            services.AddAutoMapper(typeof(AdaptersProfile));

            services.AddSingleton<NOAA.GHCND.Data.IConfiguration, Configuration>();

            services.AddSingleton<StationInfoParserRule, StationInfoParserRule>();
            services.AddSingleton<IStationSourceRule, StationFileSourceRule>();
            services.AddSingleton<HistoricClimateDatabase, HistoricClimateDatabase>();

            services.AddSingleton<NoConversionDataConversionRule, NoConversionDataConversionRule>();
            services.AddSingleton<TenthsToWholeConversionFactorRule, TenthsToWholeConversionFactorRule>();
            services.AddSingleton<IStationDatasetRule, StationDatasetRule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
