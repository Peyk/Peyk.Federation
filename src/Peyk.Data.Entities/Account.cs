using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Peyk.Data.Entities
{
    [JsonObject(
        NamingStrategyType = typeof(SnakeCaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Account
    {
        public string Id { get; set; }

        public string kind { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DeviceAccessToken[] Tokens { get; set; }
    }
}
