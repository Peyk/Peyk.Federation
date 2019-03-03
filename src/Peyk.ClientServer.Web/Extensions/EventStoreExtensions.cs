using System;
using EventStore.ClientAPI;
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
            string connectionString = configSection.GetValue<string>(nameof(EventStoreOptions.ConnectionString));
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException($@"Invalid EventStore connection string: ""{connectionString}"".");
            }

            services.Configure<EventStoreOptions>(configSection);


            services.AddScoped(provider =>
            {
                var dataOptions = provider.GetRequiredService<IOptions<EventStoreOptions>>().Value;
                return EventStoreConnection.Create(dataOptions.ConnectionString);
            });

            services.AddScoped<IEventsRepository, EventsRepository>();
        }
    }
}
