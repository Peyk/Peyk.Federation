namespace Peyk.Data.Mongo
{
    /// <summary>
    /// Contains constant values used for the MongoDB
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Contains collections information
        /// </summary>
        public static class Collections
        {
            /// <summary>
            /// Contains "rooms" collection information
            /// </summary>
            public static class Rooms
            {
                /// <summary>
                /// Collection's name
                /// </summary>
                public const string Name = "rooms";

                /// <summary>
                /// Collection's Indexes
                /// </summary>
                public static class Indexes
                {
                    /// <summary>
                    /// Index key
                    /// </summary>
                    public const string RoomId = "room_id";
                }
            }
        }
    }
}