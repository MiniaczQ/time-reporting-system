using System;

namespace lab1.Entities
{
    public class Entry
    {
        // Composite key
        public String UserName { get; set; }
        public DateTime ReportMonth { get; set; }
        public String ActivityCode { get; set; }
        public String SubactivityCode { get; set; }

        // Fields
        private DateTime _Date;
        public DateTime Date {
            get => _Date;
            set => _Date = value.Date;
        }
        public int Time { get; set; }
        public String Description { get; set; }

        // Parents
        public virtual Report Report { get; set; }
        public virtual Subcode Subcode { get; set; }
    }
}
