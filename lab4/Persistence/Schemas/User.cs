using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lab4.Persistence.Schemas
{
    public class User
    {
        public string UserName { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public static void Builder(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.UserName);
        }
        public static void Seeder(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { UserName = "Jeremy Clarkson" },
                new User { UserName = "James May" },
                new User { UserName = "Richard Hammond" }
            );
        }
    }
}
