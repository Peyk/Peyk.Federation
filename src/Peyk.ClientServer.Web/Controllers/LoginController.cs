using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Peyk.ClientServer.Commands;
using Peyk.Matrix.Models.CS.Requests;

namespace Peyk.ClientServer.Web.Controllers
{
    [Route("/_matrix/client/r0/login")]
    public class LoginController : Controller
    {
        private readonly IAuthManager _authManager;

        public LoginController(
            IAuthManager authManager
        )
        {
            _authManager = authManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] LoginOptions options = default
        )
        {
            var login = await _authManager
                .LoginAsync(options, HttpContext.RequestAborted)
                .ConfigureAwait(false);

            return Json(login);
        }
    }
}
