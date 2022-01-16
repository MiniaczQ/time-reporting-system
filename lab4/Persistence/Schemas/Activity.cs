using System.ComponentModel.DataAnnotations;
using lab4.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab4.Persistence.Schemas
{
    public class Activity
    {
        public string UserName { get; set; }
        private DateTime _ReportMonth;
        public DateTime ReportMonth { get => _ReportMonth; set => _ReportMonth = value.Dayless(); }
        public virtual Report Report { get; set; }
        public string ProjectCode { get; set; }
        public virtual Project Project { get; set; }
        public string SubprojectCode { get; set; }
        public virtual Subproject Subproject { get; set; }
        public int ActivityPid { get; set; }
        private DateTime _Date;
        public DateTime Date { get => _Date; set => _Date = value.Date; }
        public int Time { get; set; }
        public string Description { get; set; }
        public byte[] Timestamp { get; set; }
        public static void Builder(EntityTypeBuilder<Activity> builder)
        {
            builder
                .HasKey(a => new { a.ReportMonth, a.UserName, a.ProjectCode, a.ActivityPid });
            builder
                .Property(a => a.ActivityPid)
                .ValueGeneratedOnAdd();
            builder
                .Property(a => a.Date)
                .IsRequired();
            builder
                .Property(a => a.Time)
                .IsRequired();
            builder
                .Property(a => a.Description);
            builder
                .HasOne(a => a.Subproject)
                .WithMany(s => s.Activities)
                .HasForeignKey(a => new { a.ProjectCode, a.SubprojectCode });
            builder
                .HasOne(a => a.Project)
                .WithMany(p => p.Activities)
                .HasForeignKey(a => a.ProjectCode)
                .IsRequired();
            builder
                .HasOne(a => a.Report)
                .WithMany(r => r.Activities)
                .HasForeignKey(a => new { a.ReportMonth, a.UserName })
                .IsRequired();
            builder
                .Property(a => a.Timestamp)
                .IsRowVersion()
                .IsRequired();
        }
        public static void Seeder(EntityTypeBuilder<Activity> builder)
        {
            builder.HasData(
                new Activity { UserName = "Jeremy Clarkson", ReportMonth = DateTime.Now, ProjectCode = "TEST", Time = 60, Description = "Experiment", ActivityPid = -1, Date = DateTime.Now }
            );
        }
    }
}
