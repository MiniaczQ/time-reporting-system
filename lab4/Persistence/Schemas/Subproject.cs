using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab4.Persistence.Schemas
{
    public class Subproject
    {
        public string SubprojectCode { get; set; }
        public string ProjectCode { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public static void Builder(EntityTypeBuilder<Subproject> builder)
        {
            builder
                .HasKey(s => new { s.ProjectCode, s.SubprojectCode });
            builder
                .HasOne(s => s.Project)
                .WithMany(a => a.Subprojects)
                .HasForeignKey(s => s.ProjectCode)
                .IsRequired();
        }
        public static void Seeder(EntityTypeBuilder<Subproject> builder)
        {
            builder.HasData(
                new Subproject
                {
                    ProjectCode = "AUTO",
                    SubprojectCode = "Wheels"
                },
                new Subproject
                {
                    ProjectCode = "AUTO",
                    SubprojectCode = "Engine"
                },
                new Subproject
                {
                    ProjectCode = "F1",
                    SubprojectCode = "Wheels"
                },
                new Subproject
                {
                    ProjectCode = "F1",
                    SubprojectCode = "Engine"
                },
                new Subproject
                {
                    ProjectCode = "F1",
                    SubprojectCode = "Speed"
                },
                new Subproject
                {
                    ProjectCode = "TESLA",
                    SubprojectCode = "Wheels"
                },
                new Subproject
                {
                    ProjectCode = "TESLA",
                    SubprojectCode = "Engine"
                },
                new Subproject
                {
                    ProjectCode = "TESLA",
                    SubprojectCode = "Speed"
                },
                new Subproject
                {
                    ProjectCode = "TESLA",
                    SubprojectCode = "Jets"
                },
                new Subproject
                {
                    ProjectCode = "WB",
                    SubprojectCode = "Wheel"
                }
            );
        }
    }
}
