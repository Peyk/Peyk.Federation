using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Peyk.ClientServer.Queries.Models;
using Peyk.Data.Abstractions;
using Peyk.Data.Entities;
using Peyk.Data.Entities.Converters;
using Peyk.Matrix.Models.CS;

namespace Peyk.ClientServer.Queries
{
    public class RoomsQueryService : IRoomsQueryService
    {
        private readonly IPublicRoomsRepository _roomsRepo;
        private readonly ILogger _logger;

        public RoomsQueryService(
            IPublicRoomsRepository roomsRepo,
            ILogger<RoomsQueryService> logger
        )
        {
            _roomsRepo = roomsRepo;
            _logger = logger;
        }

        public async Task<PublicRoomsPaginatedResponse> GetPublicRoomsAsync(
            PublicRoomsFilter filter = default,
            CancellationToken cancellationToken = default
        )
        {
            var rooms = await _roomsRepo.GetRoomsAsync(cancellationToken)
                .ConfigureAwait(false);
            var roomsArray = rooms as Room[] ?? rooms.ToArray();
            return new PublicRoomsPaginatedResponse
            {
                NextBatch = "foo",
                Chunk = roomsArray.Select(FromEntity.ToPublicRoomChunk),
                TotalRoomCountEstimate = roomsArray.Length,
            };
        }
    }
}