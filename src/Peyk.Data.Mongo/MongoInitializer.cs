using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Peyk.Data.Entities;

namespace Peyk.Data.Mongo
{
    /// <summary>
    /// MongoDB initialization helper
    /// </summary>
    public static class MongoInitializer
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

                // create unique index "room_id" on the field "room_id"
                await roomsCollection.Indexes.CreateOneAsync(
                    new CreateIndexModel<Room>(
                        Builders<Room>.IndexKeys.Ascending(r => r.RoomId),
                        new CreateIndexOptions
                            { Name = Constants.Collections.Rooms.Indexes.RoomId, Unique = true }
                    ), cancellationToken: cancellationToken
                ).ConfigureAwait(false);
            }

            {
                // "accounts" collection
                await database
                    .CreateCollectionAsync(Constants.Collections.Accounts.Name, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                // ToDo create indices
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
                    map.MapIdProperty(r => r.Id)
                        .SetIdGenerator(UlidIdGenerator.Instance)
                        .SetSerializer(new StringSerializer(BsonType.String));
                    map.MapProperty(r => r.RoomId).SetElementName("roomId");
                    map.MapProperty(r => r.NumJoinedMembers).SetElementName("membersCount");
                    map.MapProperty(r => r.WorldReadable).SetElementName("worldReadable");
                    map.MapProperty(r => r.GuestCanJoin).SetElementName("guestCanJoin");
                    map.MapProperty(r => r.Name).SetElementName("name").SetIgnoreIfDefault(true);
                    map.MapProperty(r => r.Aliases).SetElementName("aliases").SetIgnoreIfDefault(true);
                    map.MapProperty(r => r.Topic).SetElementName("topic").SetIgnoreIfDefault(true);
                    map.MapProperty(r => r.CanonicalAlias)
                        .SetElementName("canonicalAlias").SetIgnoreIfDefault(true);
                    map.MapProperty(r => r.AvatarUrl)
                        .SetElementName("avatarUrl").SetIgnoreIfDefault(true);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Account)))
            {
                BsonClassMap.RegisterClassMap<Account>(map =>
                {
                    map.MapIdProperty(a => a.Id)
                        .SetIdGenerator(UlidIdGenerator.Instance)
                        .SetSerializer(new StringSerializer(BsonType.String));
                    map.MapProperty(a => a.Username).SetElementName("user");
                    map.MapProperty(a => a.kind).SetElementName("kind");
                    map.MapProperty(a => a.Password).SetElementName("password");
                    map.MapProperty(a => a.Tokens).SetElementName("tokens");
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(DeviceAccessToken)))
            {
                BsonClassMap.RegisterClassMap<DeviceAccessToken>(map =>
                {
                    map.MapProperty(t => t.DeviceId).SetElementName("device");
                    map.MapProperty(t => t.AccessToken).SetElementName("token");
                });
            }
        }
    }
}
