using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Peyk.Matrix.Models.CS
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
    public class PublicRoomsPaginatedResponse : PaginatedResponse<PublicRoomsChunk>
    {
        /// <summary>
        /// An estimate on the total number of public rooms, if the server has an estimate.
        /// </summary>
        public int? TotalRoomCountEstimate { get; set; }
    }
}