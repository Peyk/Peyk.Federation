using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Peyk.Data.Entities;

namespace Peyk.Data.Abstractions
{
    public interface IRoomsRepository
    {
        Task<IEnumerable<Room>> GetRoomsAsync(
            CancellationToken cancellationToken = default
        );

        Task AddAsync(
            Room room,
            CancellationToken cancellationToken = default
        );
    }
}