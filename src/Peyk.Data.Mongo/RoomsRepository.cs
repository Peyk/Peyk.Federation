using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NUlid;
using Peyk.Data.Abstractions;
using Peyk.Data.Entities;

namespace Peyk.Data.Mongo
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly IMongoCollection<Room> _collection;
        private readonly ILogger _logger;
        private FilterDefinitionBuilder<Room> Filter => Builders<Room>.Filter;

        /// <inheritdoc />
        public RoomsRepository(
            IMongoCollection<Room> collection,
            ILogger<RoomsRepository> logger
        )
        {
            _collection = collection;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Room>> GetRoomsAsync(
            CancellationToken cancellationToken = default
        )
        {
            var rooms = await _collection
                .Find(Filter.Empty)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return rooms;
        }

        /// <inheritdoc />
        public async Task AddAsync(
            Room room,
            CancellationToken cancellationToken = default
        )
        {
            await _collection.InsertOneAsync(room, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}