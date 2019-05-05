using System.Threading;
using System.Threading.Tasks;
using Peyk.Matrix.Models.CS.Requests;
using Peyk.Matrix.Models.CS.Responses;

namespace Peyk.ClientServer.Commands
{
    public interface IAccountManager
    {
        Task<CreatedAccountInfo> RegisterNewAccountAsync(
            RegisterAccountOptions options,
            CancellationToken cancellationToken = default
        );
    }
}
