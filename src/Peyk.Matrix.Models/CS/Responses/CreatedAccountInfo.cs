using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Peyk.Matrix.Models.CS.Responses
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CreatedAccountInfo
    {
        public string UserId { get; set; }

        public string AccessToken { get; set; }

        public string DeviceId { get; set; }
    }
}
