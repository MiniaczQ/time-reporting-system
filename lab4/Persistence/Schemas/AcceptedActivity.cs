using System;

namespace lab4.Persistence.Schemas
{
    public class AcceptedActivity
    {
        public string UserName { get; set; }
        public DateTime ReportMonth { get; set; }
        public virtual Report Report { get; set; }


        public string ProjectCode { get; set; }
        public virtual Project Project { get; set; }


        public int Time { get; set; } = 0;
    }
}
