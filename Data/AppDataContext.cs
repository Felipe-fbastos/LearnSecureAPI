using LearnSecureAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LearnSecureAPI.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(p => new { p.Email, p.Username })
                .IsUnique();
        }
        
        public DbSet<User> User { get; set; }


    }
}
