using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Peyk.Matrix.Models.CS.Requests
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
    public class RegisterAccountOptions
    {
        public string Kind { get; set; }

        public object Auth { get; set; }

        public bool BindEmail { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string DeviceId { get; set; }

        public string InitialDeviceDisplayName { get; set; }

        public bool InhibitLogin { get; set; }
    }
}
