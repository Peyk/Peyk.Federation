using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Peyk.Data.Abstractions;
using Peyk.Data.Entities;
using Peyk.Data.Events;
using Peyk.Matrix.Models.CS.Requests;
using Peyk.Matrix.Models.CS.Responses;

namespace Peyk.ClientServer.Commands
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountsRepository _accountsRepo;
        private readonly IEventsRepository _eventsRepo;
        private readonly ILogger _logger;

        public AccountManager(
            IAccountsRepository accountsRepo,
            IEventsRepository eventsRepo,
            ILogger<AccountManager> logger
        )
        {
            _accountsRepo = accountsRepo;
            _eventsRepo = eventsRepo;
            _logger = logger;
        }

        public async Task<CreatedAccountInfo> RegisterNewAccountAsync(
            RegisterAccountOptions options,
            CancellationToken cancellationToken = default
        )
        {
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            string token = string.Join("", Enumerable.Range(0, 20).Select(_ => (char) rnd.Next(97, 123)));
            var account = new Account
            {
                kind = options.Kind,
                Username = options.Username,
                Password = options.Password,
                Tokens = new[]
                {
                    new DeviceAccessToken { DeviceId = options.DeviceId, AccessToken = token },
                },
            };

            await _accountsRepo.AddAsync(account, cancellationToken)
                .ConfigureAwait(false);

            await _eventsRepo.AppendEventAsync(new NewAccountCreatedEvent { Account = account }, cancellationToken)
                .ConfigureAwait(false);

            return new CreatedAccountInfo
            {
                UserId = account.Id,
                DeviceId = account.Tokens[0].DeviceId,
                AccessToken = account.Tokens[0].AccessToken,
            };
        }
    }
}
