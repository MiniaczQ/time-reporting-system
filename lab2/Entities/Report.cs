using System;
using System.Collections.Generic;

namespace lab1.Entities
{
    public class Report
    {
        // Composite key
        private DateTime _ReportMonth;
        public DateTime ReportMonth
        {
            get => _ReportMonth;
            set => _ReportMonth = new DateTime(value.Year, value.Month, 0);
        }
        public String UserName { get; set; }

        // Fields
        public bool Frozen { get; set; }

        // Parents
        public virtual User User { get; set; }

        // Children
        public virtual ICollection<AcceptedEntry> AcceptedEntries { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
    }
}
