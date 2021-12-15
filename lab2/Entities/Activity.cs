using System;
using System.Collections.Generic;

namespace lab1.Entities
{
    public class Activity
    {
        // Key
        public String ActivityCode { get; set; }

        // Fields
        public int Budget { get; set; }
        public bool Active { get; set; }
        public String ManagerName { get; set; }

        // Parents
        public virtual User Manager { get; set; }

        // Children
        public virtual ICollection<AcceptedEntry> AcceptedEntries { get; set; }
        public virtual ICollection<Subcode> Subcodes { get; set; }
    }
}
