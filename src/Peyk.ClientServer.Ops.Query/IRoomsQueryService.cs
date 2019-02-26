using System.Threading;
using System.Threading.Tasks;
using Peyk.ClientServer.Ops.Query.Models;
using Peyk.Matrix.Models.CS;

namespace Peyk.ClientServer.Ops.Query
{
    public interface IRoomsQueryService
    {
        Task<PublicRoomsPaginatedResponse> GetPublicRoomsAsync(
            PublicRoomsFilter filter,
            CancellationToken cancellationToken = default
        );
    }
}