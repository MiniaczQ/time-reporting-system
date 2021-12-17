using System;
using System.Collections.Generic;

namespace lab1.Entities
{
    public class Activity
    {
        // Key
        public string ActivityCode { get; set; }

        // Fields
        public string ActivityName { get; set; }
        public int Budget { get; set; }
        public bool Active { get; set; } = true;
        public string ManagerName { get; set; }

        // Parents
        public virtual User Manager { get; set; }

        // Children
        public virtual ICollection<AcceptedEntry> AcceptedEntries { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<Subcode> Subcodes { get; set; }
    }
}
