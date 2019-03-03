using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Peyk.Data.Entities;

namespace Peyk.Data.Events
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
    public class NewRoomCreatedEvent : IEvent
    {
        [JsonIgnore]
        public string Type => Types.NewRoomCreated;

        public Room Room { get; set; }
    }
}
