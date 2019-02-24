using System.Threading;
using System.Threading.Tasks;
using Peyk.Federation.Ops.Query.Models;
using Peyk.Matrix.Models.CS;

namespace Peyk.Federation.Ops.Query
{
    public interface IRoomsQueryService
    {
        Task<PublicRoomsPaginatedResponse> GetPublicRoomsAsync(
            PublicRoomsFilter filter,
            CancellationToken cancellationToken = default
        );
    }
}