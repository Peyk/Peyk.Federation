using Peyk.Matrix.Models.CS;

namespace Peyk.Data.Entities.Converters
{
    public static class FromEntity
    {
        public static PublicRoomsChunk ToPublicRoomChunk(Room room) =>
            room == null
                ? null
                : new PublicRoomsChunk
                {
                    Id = room.RoomId,
                    Name = room.Name,
                    Topic = room.Topic,
                    Aliases = room.Aliases,
                    CanonicalAlias = room.CanonicalAlias,
                    AvatarUrl = room.AvatarUrl,
                    WorldReadable = room.WorldReadable,
                    GuestCanJoin = room.GuestCanJoin,
                    NumJoinedMembers = room.NumJoinedMembers,
                };
    }
}