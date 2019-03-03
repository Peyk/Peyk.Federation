using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoTests.Shared;
using NUlid;
using Peyk.Data.Abstractions;
using Peyk.Data.Entities;
using Peyk.Data.Mongo;
using Xunit;

namespace MongoTests
{
    [Collection("public rooms repository")]
    public class PublicRoomsRepoTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fxt;

        public PublicRoomsRepoTests(DatabaseFixture fixture)
        {
            _fxt = fixture;
        }

        [OrderedFact("should add the first room to the collection")]
        public async Task should_add_first_room()
        {
            IRoomsRepository roomsRepo = new RoomsRepository(
                _fxt.Database.GetCollection<Room>("rooms"),
                default
            );

            Room newRoom = new Room
            {
                Name = "FOO",
                RoomId = "!foo:bar.baz",
                NumJoinedMembers = 1,
                Topic = "Testing MongoDB integration",
                Aliases = new[] {"foos", "bars"},
                WorldReadable = true,
                AvatarUrl = "mxc://bar.baz/FOO-Avatar",
                CanonicalAlias = "!xyz:bar.baz",
                GuestCanJoin = true,
            };

            await roomsRepo.AddAsync(newRoom);
        }

        [OrderedFact("should query the collection and get a single room document only")]
        public async Task should_get_one_single_room_only()
        {
            IRoomsRepository roomsRepo = new RoomsRepository(
                _fxt.Database.GetCollection<Room>("rooms"),
                default
            );

            IEnumerable<Room> allRooms = await roomsRepo.GetRoomsAsync();

            Assert.NotNull(allRooms);
            Room room = Assert.Single(allRooms);

            Assert.NotNull(room);
            Assert.Equal("FOO", room.Name);
            Assert.Equal("!foo:bar.baz", room.RoomId);
            Assert.Equal(1, room.NumJoinedMembers);
            Assert.Equal("Testing MongoDB integration", room.Topic);
            Assert.Equal(new[] {"foos", "bars"}, room.Aliases);
            Assert.True(room.WorldReadable);
            Assert.True(room.GuestCanJoin);
            Assert.Equal("mxc://bar.baz/FOO-Avatar", room.AvatarUrl);
            Assert.Equal("!xyz:bar.baz", room.CanonicalAlias);
            Assert.True(Ulid.TryParse(room.Id, out Ulid ulid));
            Assert.InRange(
                ulid.Time,
                DateTimeOffset.UtcNow.AddSeconds(-10),
                DateTimeOffset.UtcNow
            );
        }

        [OrderedFact("should validate BSON schema for the room document")]
        public async Task should_validate_BSON_schema()
        {
            IMongoCollection<BsonDocument> collection = _fxt.Database.GetCollection<BsonDocument>("rooms");
            BsonDocument roomDocument = await collection
                .Find(FilterDefinition<BsonDocument>.Empty)
                .SingleAsync();

            Assert.Single(roomDocument.Elements, el => el.Name == "_id" && el.Value.IsString);
            Assert.Single(roomDocument.Elements, el => el.Name == "roomId" && el.Value.IsString);
            Assert.Single(roomDocument.Elements, el => el.Name == "name" && el.Value.IsString);
            Assert.Single(roomDocument.Elements, el => el.Name == "membersCount" && el.Value.IsInt32);
            Assert.Single(roomDocument.Elements, el => el.Name == "worldReadable" && el.Value.IsBoolean);
            Assert.Single(roomDocument.Elements, el => el.Name == "guestCanJoin" && el.Value.IsBoolean);
            Assert.Single(roomDocument.Elements, el => el.Name == "aliases" && el.Value.IsBsonArray);
            Assert.Single(roomDocument.Elements, el => el.Name == "canonicalAlias" && el.Value.IsString);
            Assert.Single(roomDocument.Elements, el => el.Name == "topic" && el.Value.IsString);
            Assert.Single(roomDocument.Elements, el => el.Name == "avatarUrl" && el.Value.IsString);
            Assert.Equal(10, roomDocument.ElementCount);
        }
    }
}