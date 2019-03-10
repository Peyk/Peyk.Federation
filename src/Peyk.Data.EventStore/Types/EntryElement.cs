using System;
using System.Collections.Generic;

namespace Peyk.Data.EventStore.Types
{
    public class EntryElement
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public DateTime Updated { get; set; }
        public PersonElement Author { get; set; }
        public string Summary { get; set; }
        public List<LinkElement> Links { get; set; }
    }
}
