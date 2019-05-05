using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Peyk.ClientServer.Commands;
using Peyk.Matrix.Models.CS.Requests;

namespace Peyk.ClientServer.Web.Controllers
{
    [Route("/_matrix/client/r0/createRoom")]
    public class CreateRoomController : Controller
    {
        private readonly IRoomManager _roomManager;

        public CreateRoomController(
            IRoomManager roomManager
        )
        {
            _roomManager = roomManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] CreateRoomOptions options = default
        )
        {
            var response = await _roomManager
                .CreateRoomsAsync(options, HttpContext.RequestAborted);

            return Json(response);
        }
    }
}
