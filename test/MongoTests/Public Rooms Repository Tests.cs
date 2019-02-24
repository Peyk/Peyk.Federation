using System.Collections.Generic;
using System.Threading.Tasks;
using Framework;
using MongoTests.Shared;
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

        [OrderedFact("Should get user 'chuck' by his username")]
        public async Task Should_Get_User_By_Name()
        {
            IPublicRoomsRepository publicRoomsRepo = new PublicRoomsRepository(
                _fxt.Database.GetCollection<Room>("rooms"),
                default
            );

            IEnumerable<Room> rooms = await publicRoomsRepo.GetRoomsAsync();
        }
    }
}