namespace lab4.Persistence.Schemas
{
    public class Subproject
    {
        // Composite key
        public string SubprojectCode { get; set; }
        public string ProjectCode { get; set; }

        // Parents
        public virtual Project Project { get; set; }

        // Children
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
