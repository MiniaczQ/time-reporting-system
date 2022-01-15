using Microsoft.EntityFrameworkCore;

using lab4.Persistence.Schemas;

namespace lab4.Persistence
{
    public class DbCtx : DbContext
    {
        public DbSet<AcceptedActivity> AcceptedAction { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Subproject> Subcodes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder
                .UseMySql("server=localhost;user=jmotyka;password=jmotyka;database=jmotyka", new MySqlServerVersion(new Version(8, 0, 27)))
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Accepted entry
            modelBuilder.Entity<AcceptedActivity>()
                .HasKey(a => new { a.ReportMonth, a.UserName, a.ProjectCode });
            modelBuilder.Entity<AcceptedActivity>()
                .Property(a => a.Time)
                .IsRequired();
            modelBuilder.Entity<AcceptedActivity>()
                .HasOne(a => a.Report)
                .WithMany(r => r.AcceptedActivities)
                .HasForeignKey(a => new { a.ReportMonth, a.UserName })
                .IsRequired();
            modelBuilder.Entity<AcceptedActivity>()
                .HasOne(a => a.Project)
                .WithMany(a => a.AcceptedActivities)
                .HasForeignKey(a => a.ProjectCode)
                .IsRequired();

            // Activity
            modelBuilder.Entity<Project>()
                .HasKey(a => a.ProjectCode);
            modelBuilder.Entity<Project>()
                .Property(a => a.ProjectName)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(a => a.Budget)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(a => a.Active)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(a => a.ManagerName)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .HasOne(a => a.Manager)
                .WithMany(u => u.Projects)
                .HasForeignKey(a => a.ManagerName)
                .IsRequired();

            // Activity
            modelBuilder.Entity<Schemas.Activity>()
                .HasKey(a => new { a.ReportMonth, a.UserName, a.ProjectCode, a.ActivityPid });
            modelBuilder.Entity<Schemas.Activity>()
                .Property(a => a.ActivityPid)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Schemas.Activity>()
                .Property(a => a.Date)
                .IsRequired();
            modelBuilder.Entity<Schemas.Activity>()
                .Property(a => a.Time)
                .IsRequired();
            modelBuilder.Entity<Schemas.Activity>()
                .Property(a => a.Description);
            modelBuilder.Entity<Schemas.Activity>()
                .HasOne(a => a.Subproject)
                .WithMany(s => s.Activities)
                .HasForeignKey(a => new { a.ProjectCode, a.SubprojectCode });
            modelBuilder.Entity<Schemas.Activity>()
                .HasOne(a => a.Project)
                .WithMany(p => p.Activities)
                .HasForeignKey(a => a.ProjectCode)
                .IsRequired();
            modelBuilder.Entity<Schemas.Activity>()
                .HasOne(a => a.Report)
                .WithMany(r => r.Activities)
                .HasForeignKey(a => new { a.ReportMonth, a.UserName })
                .IsRequired();
            modelBuilder.Entity<Schemas.Activity>()
                .Property(a => a.Timestamp)
                .IsRowVersion()
                .IsRequired();

            // Report
            modelBuilder.Entity<Report>()
                .HasKey(r => new { r.ReportMonth, r.UserName });
            modelBuilder.Entity<Report>()
                .Property(r => r.Frozen);
            modelBuilder.Entity<Report>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.UserName)
                .IsRequired();

            // Subcode
            modelBuilder.Entity<Subproject>()
                .HasKey(s => new { s.ProjectCode, s.SubprojectCode });
            modelBuilder.Entity<Subproject>()
                .HasOne(s => s.Project)
                .WithMany(a => a.Subprojects)
                .HasForeignKey(s => s.ProjectCode)
                .IsRequired();

            // User
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserName);
        }
    }
}
