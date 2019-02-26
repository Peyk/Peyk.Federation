using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Peyk.ClientServer.Ops.Query.Models;
using Peyk.Data.Abstractions;
using Peyk.Data.Entities;
using Peyk.Matrix.Models.CS;

namespace Peyk.ClientServer.Ops.Query
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
            var rooms = await _roomsRepo.GetRoomsAsync(cancellationToken);
            var roomsArray = rooms as Room[] ?? rooms.ToArray();
            return new PublicRoomsPaginatedResponse
            {
                NextBatch = "foo",
                Chunk = roomsArray.Select(r => new PublicRoomsChunk()),
                TotalRoomCountEstimate = roomsArray.Length,
            };
        }
    }
}