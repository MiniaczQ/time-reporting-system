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
                .UseMySql("server=localhost;user=app;password=app;database=app;port=54321", new MySqlServerVersion(new Version(8, 0, 27)))
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Schemas.Activity.Builder(modelBuilder.Entity<Activity>());
            Schemas.AcceptedActivity.Builder(modelBuilder.Entity<AcceptedActivity>());
            Schemas.Project.Builder(modelBuilder.Entity<Project>());
            Schemas.Report.Builder(modelBuilder.Entity<Report>());
            Schemas.Subproject.Builder(modelBuilder.Entity<Subproject>());
            Schemas.User.Builder(modelBuilder.Entity<User>());

            Schemas.Activity.Seeder(modelBuilder.Entity<Activity>());
            Schemas.AcceptedActivity.Seeder(modelBuilder.Entity<AcceptedActivity>());
            Schemas.Project.Seeder(modelBuilder.Entity<Project>());
            Schemas.Report.Seeder(modelBuilder.Entity<Report>());
            Schemas.Subproject.Seeder(modelBuilder.Entity<Subproject>());
            Schemas.User.Seeder(modelBuilder.Entity<User>());
        }
    }
}
