using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peyk.ClientServer.Commands;
using Peyk.ClientServer.Queries;
using Peyk.ClientServer.Web.Extensions;

namespace Peyk.ClientServer.Web
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
            services.AddMongoDb(Configuration.GetSection("Mongo"));
            services.AddEventStore(Configuration.GetSection("EventStore"));

            services.AddScoped<IRoomQueryService, RoomQueryService>();
            services.AddScoped<IRoomManager, RoomManager>();

            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IAuthManager, AuthManager>();

            services.AddMvc();

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.SeedUsersAsync().GetAwaiter().GetResult();
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
