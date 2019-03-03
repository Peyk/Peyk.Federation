using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Peyk.Data.Abstractions;
using Peyk.Data.Entities;
using Peyk.Data.Events;
using Peyk.Matrix.Models.CS.Requests;
using Peyk.Matrix.Models.CS.Responses;

namespace Peyk.ClientServer.Commands
{
    public class RoomsCommandService : IRoomsCommandService
    {
        private readonly IRoomsRepository _roomsRepo;
        private readonly IEventsRepository _eventsRepo;
        private readonly ILogger _logger;

        public RoomsCommandService(
            IRoomsRepository roomsRepo,
            IEventsRepository eventsRepo,
            ILogger<RoomsCommandService> logger
        )
        {
            _roomsRepo = roomsRepo;
            _eventsRepo = eventsRepo;
            _logger = logger;
        }

        public async Task<CreatedRoomInfo> CreateRoomsAsync(
            CreateRoomOptions options,
            CancellationToken cancellationToken = default
        )
        {
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            string randomId = string.Join("", Enumerable.Range(0, 10).Select(_ => (char) rnd.Next(97, 123)));
            var room = new Room
            {
                RoomId = $"!{randomId}:example.org",
                Name = "Newly Created Room",
                WorldReadable = true,
                NumJoinedMembers = 0,
                GuestCanJoin = true,
            };

            await _roomsRepo.AddAsync(room, cancellationToken)
                .ConfigureAwait(false);

            await _eventsRepo.AppendEventAsync(new NewRoomCreatedEvent { Room = room }, cancellationToken)
                .ConfigureAwait(false);

            return new CreatedRoomInfo
            {
                RoomId = room.Id
            };
        }
    }
}
