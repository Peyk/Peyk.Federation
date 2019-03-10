using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Peyk.ClientServer.Web.Options;
using Peyk.Data.Abstractions;
using Peyk.Data.EventStore;

namespace Peyk.ClientServer.Web.Extensions
{
    internal static class EventStoreExtensions
    {
        /// <summary>
        /// Adds EventStore services to the app's service collection
        /// </summary>
        public static void AddEventStore(
            this IServiceCollection services,
            IConfigurationSection configSection
        )
        {
            services.Configure<EventStoreOptions>(configSection);

            services.AddSingleton(provider =>
            {
                var options = provider.GetRequiredService<IOptions<EventStoreOptions>>().Value;
                return new ConnectionConfigs
                {
                    Url = options.Url,
                    User = options.Username,
                    Password = options.Password,
                };
            });

            services.AddScoped<IEventsRepository, EventsRepository>();
        }
    }
}
