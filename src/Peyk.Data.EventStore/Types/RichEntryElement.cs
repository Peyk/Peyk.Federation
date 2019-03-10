using System;

namespace Peyk.Data.EventStore.Types
{
    public class RichEntryElement : EntryElement
    {
        public Guid EventId { get; set; }
        public string EventType { get; set; }
        public long EventNumber { get; set; }
        public string Data { get; set; }
        public string MetaData { get; set; }
        public string LinkMetaData { get; set; }
        public string StreamId { get; set; }
        public bool IsJson { get; set; }
        public bool IsMetaData { get; set; }
        public bool IsLinkMetaData { get; set; }
        public long PositionEventNumber { get; set; }
        public string PositionStreamId { get; set; }
    }
}