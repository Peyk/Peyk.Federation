using System.Collections.Generic;

namespace Peyk.Data.Events
{
    public static class StreamNames
    {
        public static readonly IReadOnlyDictionary<string, string> Mappings = new Dictionary<string, string>
        {
            { Types.NewRoomCreated, "rooms" },
            { Types.NewAccountCreated, "accounts" },
        };

//        public static string GetStreamNameForEvent(IEvent e)
        public static string GetStreamNameForEventType(string eventType)
        {
            string stream;
            if (eventType.StartsWith("room:"))
            {
                stream = eventType.Substring("room:".Length);
            }
            else
            {
                stream = Mappings[eventType];
            }

            return stream;
        }
    }
}
