using System.Threading;
using System.Threading.Tasks;
using Peyk.ClientServer.Queries.Models;
using Peyk.Matrix.Models.CS;

namespace Peyk.ClientServer.Queries
{
    public interface IRoomQueryService
    {
        Task<PublicRoomsPaginatedResponse> GetPublicRoomsAsync(
            PublicRoomsFilter filter,
            CancellationToken cancellationToken = default
        );
    }
}
