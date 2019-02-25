using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Peyk.Data.Entities
{
    [JsonObject(
        NamingStrategyType = typeof(SnakeCaseNamingStrategy),
        ItemNullValueHandling = NullValueHandling.Ignore
    )]
    public class Room
    {
        /// <summary>
        /// Entity's unique ID in the data storage 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Required. The ID of the room.
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// Required. The number of members joined to the room.
        /// </summary>
        public int NumJoinedMembers { get; set; }

        /// <summary>
        /// Required. Whether the room may be viewed by guest users without joining.
        /// </summary>
        public bool WorldReadable { get; set; }

        /// <summary>
        /// Required. Whether guest users may join the room and participate in it.
        /// If they can, they will be subject to ordinary power level rules like any other user.
        /// </summary>
        public bool GuestCanJoin { get; set; }

        /// <summary>
        /// Aliases of the room. May be empty.
        /// </summary>
        public string[] Aliases { get; set; }

        /// <summary>
        /// The canonical alias of the room, if any.
        /// </summary>
        public string CanonicalAlias { get; set; }

        /// <summary>
        /// The name of the room, if any.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The topic of the room, if any.
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// The URL for the room's avatar, if one is set.
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}