namespace lab4.Models
{
    public class UserAll
    {
        public string UserName { get; set; }
    }

    public class ActivityAdd
    {
        public string ProjectCode { get; set; }
        public DateTime Date { get; set; }
        public string SubprojectCode { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
    }

    public class ActivityAll
    {
        public int ActivityPid { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }
        public string SubprojectCode { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
    }

    public class ReportAll
    {
        public List<ActivityAll> Activities { get; set; }
        public bool Frozen { get; set; }
    }

    public class ProjectAll
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
    }
}
