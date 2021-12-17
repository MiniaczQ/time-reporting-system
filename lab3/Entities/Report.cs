using System;
using System.Collections.Generic;
using lab1.Datetime;

namespace lab1.Entities
{
    public class Report
    {
        // Composite key
        private DateTime _ReportMonth;
        public DateTime ReportMonth
        {
            get => _ReportMonth;
            set => _ReportMonth = value.Dayless();
        }
        public string UserName { get; set; }

        // Fields
        public bool Frozen { get; set; } = false;

        // Parents
        public virtual User User { get; set; }

        // Children
        public virtual ICollection<AcceptedEntry> AcceptedEntries { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
    }
}
