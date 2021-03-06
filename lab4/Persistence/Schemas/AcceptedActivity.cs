using lab4.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab4.Persistence.Schemas
{
    public class AcceptedActivity
    {
        public string UserName { get; set; }
        private DateTime _ReportMonth;
        public DateTime ReportMonth { get => _ReportMonth; set => _ReportMonth = value.Dayless(); }
        public virtual Report Report { get; set; }
        public string ProjectCode { get; set; }
        public virtual Project Project { get; set; }
        public int Time { get; set; } = 0;
        public static void Builder(EntityTypeBuilder<AcceptedActivity> builder)
        {
            builder
                .HasKey(a => new { a.ReportMonth, a.UserName, a.ProjectCode });
            builder
                .Property(a => a.Time)
                .IsRequired();
            builder
                .HasOne(a => a.Report)
                .WithMany(r => r.AcceptedActivities)
                .HasForeignKey(a => new { a.ReportMonth, a.UserName })
                .IsRequired();
            builder
                .HasOne(a => a.Project)
                .WithMany(a => a.AcceptedActivities)
                .HasForeignKey(a => a.ProjectCode)
                .IsRequired();
        }
        public static void Seeder(EntityTypeBuilder<AcceptedActivity> builder)
        {
            builder.HasData(
                new AcceptedActivity
                {
                    ReportMonth = new DateTime(2022, 1, 1),
                    UserName = "Jeremy Clarkson",
                    ProjectCode = "WB",
                    Time = 5
                }
            );
        }
    }
}
