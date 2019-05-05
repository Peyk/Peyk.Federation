using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Peyk.ClientServer.Queries;

namespace Peyk.ClientServer.Web.Controllers
{
    [Route("/_matrix/client/r0/publicRooms")]
    public class PublicRoomsController : Controller
    {
        private readonly IRoomQueryService _roomQueryService;

        public PublicRoomsController(
            IRoomQueryService roomQueryService
        )
        {
            _roomQueryService = roomQueryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string limit = default,
            [FromQuery] string since = default,
            [FromQuery] string server = default
        )
        {
            var paginatedResponse = await _roomQueryService
                .GetPublicRoomsAsync(default, HttpContext.RequestAborted);

            return Json(paginatedResponse);
        }
    }
}
