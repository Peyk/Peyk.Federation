using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peyk.Federation.Ops.Query;
using Peyk.Federation.Web.Extensions;

namespace Peyk.Federation.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        /// <inheritdoc />
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMongoDb(Configuration.GetSection("mongo"));

            services.AddTransient<IRoomsQueryService, RoomsQueryService>();

            services.AddMvc();

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(cors => cors
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetPreflightMaxAge(TimeSpan.FromDays(7))
            );

            app.UseMvc();

            app.Run(async context => { await context.Response.WriteAsync("Hello World!"); });
        }
    }
}