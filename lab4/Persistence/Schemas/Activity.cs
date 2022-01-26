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
        public int? ActivityPid { get; set; }
        private DateTime _Date;
        public DateTime Date { get => _Date; set => _Date = value.Date; }
        public int Time { get; set; }
        public string Description { get; set; }
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
                .HasForeignKey(a => new { a.ProjectCode, a.SubprojectCode })
                .IsRequired(false);
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
        }
        public static void Seeder(EntityTypeBuilder<Activity> builder)
        {
            builder.HasData(
                new Activity
                {
                    ActivityPid = -1,
                    UserName = "Jeremy Clarkson",
                    ReportMonth = new DateTime(2022, 1, 1),
                    ProjectCode = "AUTO",
                    Date = new DateTime(2022, 01, 25),
                    Time = 35,
                    Description = "desc 1",
                },
                new Activity
                {
                    ActivityPid = -2,
                    UserName = "Jeremy Clarkson",
                    ReportMonth = new DateTime(2022, 1, 1),
                    ProjectCode = "AUTO",
                    SubprojectCode = "Wheels",
                    Date = new DateTime(2022, 01, 25),
                    Time = 75,
                    Description = "desc 2",
                },
                new Activity
                {
                    ActivityPid = -3,
                    UserName = "James May",
                    ReportMonth = new DateTime(2022, 1, 1),
                    ProjectCode = "F1",
                    SubprojectCode = "Engine",
                    Date = new DateTime(2022, 01, 26),
                    Time = 15,
                    Description = "desc 3",
                },
                new Activity
                {
                    ActivityPid = -4,
                    UserName = "James May",
                    ReportMonth = new DateTime(2022, 1, 1),
                    ProjectCode = "F1",
                    SubprojectCode = "Speed",
                    Date = new DateTime(2022, 01, 26),
                    Time = 10,
                    Description = "desc 4",
                },
                new Activity
                {
                    ActivityPid = -5,
                    UserName = "Jeremy Clarkson",
                    ReportMonth = new DateTime(2022, 1, 1),
                    ProjectCode = "TESLA",
                    SubprojectCode = "Jets",
                    Date = new DateTime(2022, 01, 26),
                    Time = 20,
                    Description = "desc 5",
                },
                new Activity
                {
                    ActivityPid = -6,
                    UserName = "Jeremy Clarkson",
                    ReportMonth = new DateTime(2022, 1, 1),
                    ProjectCode = "TESLA",
                    Date = new DateTime(2022, 01, 26),
                    Time = 30,
                    Description = "desc 6",
                },
                new Activity
                {
                    ActivityPid = -7,
                    UserName = "Richard Hammond",
                    ReportMonth = new DateTime(2022, 1, 1),
                    ProjectCode = "TESLA",
                    SubprojectCode = "Speed",
                    Date = new DateTime(2022, 01, 27),
                    Time = 30,
                    Description = "desc 7",
                },
                new Activity
                {
                    ActivityPid = -9,
                    UserName = "Richard Hammond",
                    ReportMonth = new DateTime(2022, 1, 1),
                    ProjectCode = "WB",
                    Date = new DateTime(2022, 01, 27),
                    Time = 15,
                    Description = "desc 9",
                },
                new Activity
                {
                    ActivityPid = -10,
                    UserName = "Jeremy Clarkson",
                    ReportMonth = new DateTime(2022, 1, 1),
                    ProjectCode = "WB",
                    SubprojectCode = "Wheel",
                    Date = new DateTime(2022, 01, 27),
                    Time = 10,
                    Description = "desc 10",
                }
            );
        }
    }
}
