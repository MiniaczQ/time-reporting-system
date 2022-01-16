using lab4.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab4.Persistence.Schemas
{
    public class Report
    {
        private DateTime _ReportMonth;
        public DateTime ReportMonth { get => _ReportMonth; set => _ReportMonth = value.Dayless(); }
        public string UserName { get; set; }
        public bool Frozen { get; set; } = false;
        public virtual User User { get; set; }
        public virtual ICollection<AcceptedActivity> AcceptedActivities { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public static void Builder(EntityTypeBuilder<Report> builder)
        {
            builder
                .HasKey(r => new { r.ReportMonth, r.UserName });
            builder
                .Property(r => r.Frozen);
            builder
                .HasOne(r => r.User)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.UserName)
                .IsRequired();
        }
        public static void Seeder(EntityTypeBuilder<Report> builder)
        {
            builder.HasData(
                new Report { ReportMonth = DateTime.Now, UserName = "Jeremy Clarkson" }
            );
        }
    }
}
