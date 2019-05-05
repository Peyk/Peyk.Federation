using System.Threading;
using System.Threading.Tasks;
using Peyk.Data.Entities;

namespace Peyk.Data.Abstractions
{
    public interface IAccountsRepository
    {
        Task AddAsync(
            Account account,
            CancellationToken cancellationToken = default
        );

        Task<Account> GetAccountByUserPasswordAsync(
            string user,
            string password,
            CancellationToken cancellationToken = default
        );
    }
}
