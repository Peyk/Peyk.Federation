using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Peyk.ClientServer.Commands;
using Peyk.Matrix.Models.CS;
using Peyk.Matrix.Models.CS.Requests;

namespace Peyk.ClientServer.Web.Extensions
{
    internal static class DataSeedExtensions
    {
        public static async Task SeedUsersAsync(
            this IApplicationBuilder app
        )
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
                var accountManager = scope.ServiceProvider.GetRequiredService<IAccountManager>();
                var authManager = scope.ServiceProvider.GetRequiredService<IAuthManager>();

                var login = await authManager.LoginAsync(new LoginOptions
                {
                    Identifier = new UserIdentifier { User = "alice" },
                    Password = "password1"
                });

                if (login != null)
                {
                    logger.LogInformation("User already exists. Skipping the accounts seed.");
                    return;
                }

                await accountManager.RegisterNewAccountAsync(new RegisterAccountOptions
                {
                    Username = "alice",
                    Password = "password1",
                    Kind = Enums.UserAccount.User,
                });
                await accountManager.RegisterNewAccountAsync(new RegisterAccountOptions
                {
                    Username = "bob",
                    Password = "password2",
                    Kind = Enums.UserAccount.User,
                });
                await accountManager.RegisterNewAccountAsync(new RegisterAccountOptions
                {
                    Username = "chris",
                    Password = "password3",
                    Kind = Enums.UserAccount.User,
                });
            }
        }
    }
}
