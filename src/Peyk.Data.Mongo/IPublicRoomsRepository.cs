using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Peyk.Data.Entities;

namespace Peyk.Data.Mongo
{
    public interface IPublicRoomsRepository
    {
        Task<IEnumerable<Room>> GetRoomsAsync(
            CancellationToken cancellationToken = default
        );
    }
}