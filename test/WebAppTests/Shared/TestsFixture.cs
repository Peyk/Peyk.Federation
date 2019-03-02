using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Peyk.Data.Mongo;

namespace WebAppTests.Shared
{
    public class TestsFixture
    {
        public HttpClient HttpClient { get; }

        public IServiceProvider Services => WebAppFactory.Server.Host.Services;

        public WebAppFactory WebAppFactory { get; }

        public TestsFixture()
        {
            WebAppFactory = new WebAppFactory();
            HttpClient = WebAppFactory.CreateClient();

            EnsureEmptyDatabase();
            InitializeDatabase().GetAwaiter().GetResult();
        }

        private void EnsureEmptyDatabase()
        {
            var db = Services.GetRequiredService<IMongoDatabase>();

            var collections = db.ListCollectionNames().ToList();

            foreach (var collection in collections)
            {
                db.DropCollection(collection);
            }
        }

        private async Task InitializeDatabase()
        {
            var db = Services.GetRequiredService<IMongoDatabase>();

            Initializer.RegisterClassMaps();
            await Initializer.CreateSchemaAsync(db)
                .ConfigureAwait(false);
        }
    }
}