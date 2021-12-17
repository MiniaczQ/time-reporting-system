using System;

namespace lab1.Entities
{
    public class AcceptedEntry
    {
        // Composite key
        public string UserName { get; set; }
        public string ActivityCode { get; set; }
        public DateTime ReportMonth { get; set; }

        // Fields
        public int Time { get; set; } = 0;

        // Parents
        public virtual Report Report { get; set; }
        public virtual Activity Activity { get; set; }
    }
}
