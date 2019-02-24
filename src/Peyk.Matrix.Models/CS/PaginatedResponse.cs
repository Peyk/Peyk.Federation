using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Peyk.Matrix.Models.CS
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
    public class PaginatedResponse<T>
    {
        /// <summary>
        /// Required. A paginated chunk of public rooms.
        /// </summary>
        public IEnumerable<T> Chunk { get; set; }

        /// <summary>
        /// A pagination token for the response. The absence of this token means there are no more results to fetch
        /// and the client should stop paginating.
        /// </summary>
        public string NextBatch { get; set; }

        /// <summary>
        /// A pagination token that allows fetching previous results. The absence of this token means there are
        /// no results before this batch, i.e. this is the first batch.
        /// </summary>
        public string PrevBatch { get; set; }
    }
}