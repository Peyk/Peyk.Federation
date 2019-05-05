using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Peyk.Data.Abstractions;
using Peyk.Matrix.Models.CS;
using Peyk.Matrix.Models.CS.Requests;
using Peyk.Matrix.Models.CS.Responses;

namespace Peyk.ClientServer.Commands
{
    public class AuthManager : IAuthManager
    {
        private readonly IAccountsRepository _accountsRepo;
        private readonly IEventsRepository _eventsRepo;
        private readonly ILogger _logger;

        public AuthManager(
            IAccountsRepository accountsRepo,
            IEventsRepository eventsRepo,
            ILogger<AuthManager> logger
        )
        {
            _accountsRepo = accountsRepo;
            _eventsRepo = eventsRepo;
            _logger = logger;
        }

        public async Task<LoginInfo> LoginAsync(
            LoginOptions options,
            CancellationToken cancellationToken = default
        )
        {
            var rnd = new Random(DateTime.UtcNow.Millisecond);
            string token = string.Join("", Enumerable.Range(0, 20).Select(_ => (char) rnd.Next(97, 123)));

            var account = await _accountsRepo.GetAccountByUserPasswordAsync(
                options.Identifier.User,
                options.Password, cancellationToken
            ).ConfigureAwait(false);

            // ToDo write the event

            // ToDo handle errors

            return account == null
                ? null
                : new LoginInfo
                {
                    UserId = account.Username,
                    AccessToken = token,
                    DeviceId = "foo",
                };
        }
    }
}
