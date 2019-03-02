using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Framework;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Newtonsoft.Json;
using WebAppTests.Shared;
using Xunit;

namespace WebAppTests
{
    public class RoomQueriesTests : IClassFixture<RoomQueriesTests.Fixture>
    {
        private readonly TestsFixture _fxt;

        public RoomQueriesTests(Fixture fxt)
        {
            _fxt = fxt;
        }

        public class Fixture : TestsFixture
        {
            public Fixture()
            {
                TestData.SeedRooms(Services.GetRequiredService<IMongoDatabase>());
            }
        }

        [OrderedFact("Should get public rooms chunk")]
        public async Task Should_Get_Rooms()
        {
            HttpResponseMessage response = await _fxt.HttpClient.GetAsync("/_matrix/client/r0/publicRooms");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string responseContent = await response.Content.ReadAsStringAsync();
            Asserts.IsJson(responseContent);

            dynamic responseBody = JsonConvert.DeserializeObject(responseContent);

            Assert.Equal(1, (int) responseBody.total_room_count_estimate);
            Assert.Equal("foo", (string) responseBody.next_batch);
            Assert.NotNull(responseBody.chunk);
        }
    }
}