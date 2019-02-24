using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Peyk.Data.Entities;
using Peyk.Data.Mongo;
using Peyk.Federation.Web.Options;

namespace Peyk.Federation.Web.Extensions
{
    internal static class MongoDbExtensions
    {
        /// <summary>
        /// Adds MongoDB services to the app's service collection
        /// </summary>
        public static void AddMongoDb(
            this IServiceCollection services,
            IConfigurationSection dataSection
        )
        {
            string connectionString = dataSection.GetValue<string>(nameof(MongoDbOptions.ConnectionString));
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException($@"Invalid MongoDB connection string: ""{connectionString}"".");
            }

            services.Configure<MongoDbOptions>(dataSection);

            string dbName = new ConnectionString(connectionString).DatabaseName;
            services.AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient(connectionString));
            services.AddTransient<IMongoDatabase>(provider =>
                provider.GetRequiredService<IMongoClient>().GetDatabase(dbName)
            );

            services.AddTransient<IMongoCollection<Room>>(_ =>
                _.GetRequiredService<IMongoDatabase>()
                    .GetCollection<Room>(Constants.Collections.Rooms.Name)
            );

            services.AddTransient<IPublicRoomsRepository, PublicRoomsRepository>();

            Initializer.RegisterClassMaps();
        }
    }
}