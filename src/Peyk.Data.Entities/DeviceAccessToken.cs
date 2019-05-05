using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Peyk.Data.Entities
{
    [JsonObject(
        NamingStrategyType = typeof(SnakeCaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class DeviceAccessToken
    {
        public string DeviceId { get; set; }

        public string AccessToken { get; set; }
    }
}
