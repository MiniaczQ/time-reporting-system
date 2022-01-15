using System.ComponentModel.DataAnnotations;

namespace lab4.Persistence.Schemas
{
    public class Activity
    {
        public string UserName { get; set; }
        public DateTime ReportMonth { get; set; }
        public virtual Report Report { get; set; }


        public string ProjectCode { get; set; }
        public virtual Project Project { get; set; }

        public string SubprojectCode { get; set; }
        public virtual Subproject Subproject { get; set; }


        public int ActivityPid { get; set; }
        private DateTime _Date;
        [DataType(DataType.Date)]
        public DateTime Date { get => _Date; set => _Date = value.Date; }
        public int Time { get; set; }
        public string Description { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
