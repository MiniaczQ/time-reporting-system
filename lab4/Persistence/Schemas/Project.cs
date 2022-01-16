using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab4.Persistence.Schemas
{
    public class Project
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public int Budget { get; set; }
        public bool Active { get; set; } = true;
        public string ManagerName { get; set; }
        public virtual User Manager { get; set; }
        public virtual ICollection<AcceptedActivity> AcceptedActivities { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Subproject> Subprojects { get; set; }
        public static void Builder(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasKey(a => a.ProjectCode);
            builder
                .Property(a => a.ProjectName)
                .IsRequired();
            builder
                .Property(a => a.Budget)
                .IsRequired();
            builder
                .Property(a => a.Active)
                .IsRequired();
            builder
                .Property(a => a.ManagerName)
                .IsRequired();
            builder
                .HasOne(a => a.Manager)
                .WithMany(u => u.Projects)
                .HasForeignKey(a => a.ManagerName)
                .IsRequired();
        }
        public static void Seeder(EntityTypeBuilder<Project> builder)
        {
            builder.HasData(
                new Project { ProjectCode = "TEST", ProjectName = "Test", Budget = 120, ManagerName = "Jeremy Clarkson" }
            );
        }
    }
}
