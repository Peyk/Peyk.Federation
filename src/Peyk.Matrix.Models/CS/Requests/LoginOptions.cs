using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Peyk.Matrix.Models.CS.Requests
{
    [JsonObject(
        NamingStrategyType = typeof(SnakeCaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class LoginOptions
    {
        public string Type { get; set; }

        public UserIdentifier Identifier { get; set; }

        public string Password { get; set; }
    }
}
