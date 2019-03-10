using System;
using System.Collections.Generic;

namespace Peyk.Data.EventStore.Types
{
    public class FeedElement
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public DateTime Updated { get; set; }
        public string StreamId { get; set; }
        public PersonElement Author { get; set; }
        public bool HeadOfStream { get; set; }
        public string SelfUrl { get; set; }
        public string ETag { get; set; }
        public List<LinkElement> Links { get; set; }
        public List<EntryElement> Entries { get; set; }
    }
}
