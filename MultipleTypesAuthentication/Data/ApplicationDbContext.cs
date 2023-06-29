using Microsoft.EntityFrameworkCore;
using MultipleTypesAuthentication.Domain;

namespace MultipleTypesAuthentication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("User").HasIndex(e => e.Email).IsUnique().HasName("EmailIndex");      

        }

    }
}
