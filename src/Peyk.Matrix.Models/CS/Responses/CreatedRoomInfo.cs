using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Peyk.Matrix.Models.CS.Responses
{
    /// <summary>
    /// Information about the newly created room.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CreatedRoomInfo
    {
        /// <summary>
        /// Required. The created room's ID.
        /// </summary>
        public string RoomId { get; set; }
    }
}