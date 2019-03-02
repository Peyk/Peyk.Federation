using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Peyk.ClientServer.Web;

namespace WebAppTests.Shared
{
    public class WebAppFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureTestServices(ConfigureServices);
        }

        private void ConfigureServices(IServiceCollection services)
        {
        }
    }
}