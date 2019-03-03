using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Peyk.ClientServer.Queries;

namespace Peyk.ClientServer.Web.Controllers
{
    [Route("/_matrix/client/r0/publicRooms")]
    public class PublicRoomsController : Controller
    {
        private readonly IRoomsQueryService _roomsQueryService;

        public PublicRoomsController(
            IRoomsQueryService roomsQueryService
        )
        {
            _roomsQueryService = roomsQueryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string limit = default,
            [FromQuery] string since = default,
            [FromQuery] string server = default
        )
        {
            var paginatedResponse = await _roomsQueryService
                .GetPublicRoomsAsync(default, HttpContext.RequestAborted);

            return Json(paginatedResponse);
        }
    }
}