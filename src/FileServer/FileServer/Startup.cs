using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileServer.Config;
using FileServer.Services.SoftwareProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FileServer
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
            // Options
            services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
            services.Configure<FileScanConfig>(Configuration.GetSection(nameof(FileScanConfig)));

            // Dependencies
            _ = services.AddTransient<IProvideSoftware, GetHighestSoftwareVersion>();
            _ = services.AddTransient<IProvideFilesInDirectory, ProvideFilesInDirectory>();
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
