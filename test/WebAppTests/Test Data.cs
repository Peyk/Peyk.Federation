using MongoDB.Bson;
using MongoDB.Driver;

namespace WebAppTests
{
    public static class TestData
    {
        public static void SeedRooms(IMongoDatabase db)
        {
            var usersCollection = db.GetCollection<BsonDocument>("rooms");

            usersCollection.InsertMany(new[]
            {
                BsonDocument.Parse(@"{
                    _id : ""01D4ZT17310VNDRHCR0ZYTA6ZF"",
                    roomId : ""!foo:bar.baz"",
                    name : ""FOO"",
                    membersCount : 1,
                    worldReadable : true,
                    guestCanJoin : true,
                    aliases : [ ""bars"" ],
                    canonicalAlias : ""!xyz:bar.baz"",
                    topic : ""Testing Client-Server API integration"",
                    avatarUrl : ""mxc://bar.baz/FOO-Avatar""
                }"),
            });
        }
    }
}