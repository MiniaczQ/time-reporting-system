using System;
using System.Collections.Generic;

namespace lab1.Entities
{
    public class Subcode
    {
        // Composite key
        public string SubactivityCode { get; set; }
        public string ActivityCode { get; set; }

        // Parents
        public virtual Activity Activity { get; set; }

        // Children
        public virtual ICollection<Entry> Entries { get; set; }
    }
}
