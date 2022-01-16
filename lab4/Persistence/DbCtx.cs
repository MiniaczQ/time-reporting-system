using Microsoft.EntityFrameworkCore;

using lab4.Persistence.Schemas;

namespace lab4.Persistence
{
    public class DbCtx : DbContext
    {
        public DbSet<AcceptedActivity> AcceptedActivities { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Subproject> Subprojects { get; set; }
        public DbSet<User> Users { get; set; }

        private static string ConnectionString = "server=localhost;user=app;password=app;database=app;port=54321";
        private static MySqlServerVersion MySQLVersion = new MySqlServerVersion(new Version(8, 0, 27));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder
                .UseMySql(ConnectionString, MySQLVersion)
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Schemas.User.Builder(modelBuilder.Entity<User>());
            Schemas.Project.Builder(modelBuilder.Entity<Project>());
            Schemas.Subproject.Builder(modelBuilder.Entity<Subproject>());
            Schemas.Report.Builder(modelBuilder.Entity<Report>());
            Schemas.Activity.Builder(modelBuilder.Entity<Activity>());
            Schemas.AcceptedActivity.Builder(modelBuilder.Entity<AcceptedActivity>());

            Schemas.User.Seeder(modelBuilder.Entity<User>());
            Schemas.Project.Seeder(modelBuilder.Entity<Project>());
            Schemas.Subproject.Seeder(modelBuilder.Entity<Subproject>());
            Schemas.Report.Seeder(modelBuilder.Entity<Report>());
            Schemas.Activity.Seeder(modelBuilder.Entity<Activity>());
            Schemas.AcceptedActivity.Seeder(modelBuilder.Entity<AcceptedActivity>());
        }
    }
}
