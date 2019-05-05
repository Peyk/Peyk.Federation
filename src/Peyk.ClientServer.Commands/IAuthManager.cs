using System.Threading;
using System.Threading.Tasks;
using Peyk.Matrix.Models.CS.Requests;
using Peyk.Matrix.Models.CS.Responses;

namespace Peyk.ClientServer.Commands
{
    public interface IAuthManager
    {
        Task<LoginInfo> LoginAsync(
            LoginOptions options,
            CancellationToken cancellationToken = default
        );
    }
}
