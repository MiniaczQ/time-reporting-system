using lab4.Utility;

namespace lab4.Persistence.Schemas
{
    public class Report
    {
        // Composite key
        private DateTime _ReportMonth;
        public DateTime ReportMonth { get => _ReportMonth; set => _ReportMonth = value.Dayless(); }
        public string UserName { get; set; }

        // Fields
        public bool Frozen { get; set; } = false;

        // Parents
        public virtual User User { get; set; }

        // Children
        public virtual ICollection<AcceptedActivity> AcceptedActivities { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
