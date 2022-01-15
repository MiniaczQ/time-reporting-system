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
        }
    }
}
