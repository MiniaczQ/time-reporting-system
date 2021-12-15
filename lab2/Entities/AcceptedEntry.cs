using System;

namespace lab1.Entities
{
    public class AcceptedEntry
    {
        // Composite key
        public String UserName { get; set; }
        public String ActivityCode { get; set; }
        public DateTime ReportMonth { get; set; }

        // Fields
        public int Time { get; set; }

        // Parents
        public virtual Report Report { get; set; }
        public virtual Activity Activity { get; set; }
    }
}
