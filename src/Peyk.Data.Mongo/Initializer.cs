using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Peyk.Data.Entities;

namespace Peyk.Data.Mongo
{
    /// <summary>
    /// MongoDB initialization helper
    /// </summary>
    public static class Initializer
    {
        /// <summary>
        /// Creates the database schema
        /// </summary>
        /// <param name="database">Database instance</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        public static async Task CreateSchemaAsync(
            IMongoDatabase database,
            CancellationToken cancellationToken = default
        )
        {
            {
                // "rooms" collection
                await database
                    .CreateCollectionAsync(Constants.Collections.Rooms.Name, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
                var roomsCollection = database.GetCollection<Room>(Constants.Collections.Rooms.Name);

                // create unique index "room_id" on the field "id"
                await roomsCollection.Indexes.CreateOneAsync(new CreateIndexModel<Room>(
                        Builders<Room>.IndexKeys.Ascending(r => r.Id),
                        new CreateIndexOptions
                            {Name = Constants.Collections.Rooms.Indexes.RoomId, Unique = true}),
                    cancellationToken: cancellationToken
                ).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Registers all the mappings between data entities and the documents stored in MongoDB collections
        /// </summary>
        public static void RegisterClassMaps()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Room)))
            {
                BsonClassMap.RegisterClassMap<Room>(map =>
                {
                    map.MapIdProperty(r => r.Id);
                    map.MapProperty(r => r.NumJoinedMembers).SetElementName("members_count");
                    map.MapProperty(r => r.CreatedAt).SetElementName("created_at");
                });
            }
        }
    }
}