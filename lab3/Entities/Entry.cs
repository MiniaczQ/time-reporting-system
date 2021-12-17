using System;
using System.ComponentModel.DataAnnotations;

namespace lab1.Entities
{
    public class Entry
    {
        // Composite key
        public int EntryPid { get; set; }
        public string UserName { get; set; }
        public DateTime ReportMonth { get; set; }
        public string ActivityCode { get; set; }

        // Fields
        private DateTime _Date;
        [DataType(DataType.Date)]
        public DateTime Date
        {
            get => _Date;
            set => _Date = value.Date;
        }
        public int Time { get; set; }
        public string Description { get; set; }

        // Foreign
        public string SubactivityCode { get; set; }

        // Parents
        public virtual Report Report { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual Subcode Subcode { get; set; }

        // Concurrency
        public byte[] Timestamp { get; set; }
    }
}
