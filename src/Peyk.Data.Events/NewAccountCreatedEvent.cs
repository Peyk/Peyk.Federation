using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Peyk.Data.Entities;

namespace Peyk.Data.Events
{
    [JsonObject(
        NamingStrategyType = typeof(CamelCaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class NewAccountCreatedEvent : IEvent
    {
        [JsonIgnore]
        public string Type => Types.NewAccountCreated;

        public Account Account { get; set; }
    }
}
