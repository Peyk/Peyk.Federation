using System.Collections.Generic;

namespace Peyk.Data.Events
{
    public static class StreamNames
    {
        public static readonly IReadOnlyDictionary<string, string> Mappings = new Dictionary<string, string>
        {
            { Types.NewRoomCreated, "rooms" }
        };

        public static string GetStreamNameForEventType(string eventType) => Mappings[eventType];
    }
}
