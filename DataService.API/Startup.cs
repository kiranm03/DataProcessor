using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using DataService.AWSServices;
using DataService.Mappers;
using DataService.Model;
using DataService.Workers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TinyCsvParser.Mapping;

namespace DataService
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddDefaultAWSOptions(Configuration.GetAWSOptions());

            services.AddSingleton<IDataAggregator<LPData>, LPDataAggregator>();
            services.AddSingleton<IDataAggregator<TOUData>, TOUDataAggregator>();
            services.AddSingleton<IDataLoader<LPData>, LPDataLoader>();
            services.AddSingleton<IDataLoader<TOUData>, TOUDataLoader>();
            services.AddSingleton<ICsvMapping<LPData>, CsvLPDataMapping>();
            services.AddSingleton<ICsvMapping<TOUData>, CsvTOUDataMapping>();
            services.AddSingleton<IS3Service, S3Service>();

            services.AddAWSService<IAmazonS3>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
