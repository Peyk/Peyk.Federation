using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Peyk.Data.Abstractions;
using Peyk.Data.Entities;

namespace Peyk.Data.Mongo
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IMongoCollection<Account> _collection;
        private readonly ILogger _logger;
        private FilterDefinitionBuilder<Account> Filter => Builders<Account>.Filter;

        /// <inheritdoc />
        public AccountsRepository(
            IMongoCollection<Account> collection,
            ILogger<AccountsRepository> logger
        )
        {
            _collection = collection;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task AddAsync(
            Account account,
            CancellationToken cancellationToken = default
        )
        {
            await _collection.InsertOneAsync(account, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<Account> GetAccountByUserPasswordAsync(
            string user,
            string password,
            CancellationToken cancellationToken = default
        )
        {
            var filter = Filter.And(
                Filter.Eq(a => a.Username, user),
                Filter.Eq(a => a.Password, password)
            );

            var account = await _collection
                .Find(filter)
                .SingleOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            return account;
        }
    }
}
