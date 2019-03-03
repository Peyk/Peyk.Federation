using System.Threading;
using System.Threading.Tasks;
using Peyk.Matrix.Models.CS.Requests;
using Peyk.Matrix.Models.CS.Responses;

namespace Peyk.ClientServer.Commands
{
    public interface IRoomsCommandService
    {
        Task<CreatedRoomInfo> CreateRoomsAsync(
            CreateRoomOptions options,
            CancellationToken cancellationToken = default
        );
    }
}