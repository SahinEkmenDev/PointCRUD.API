using Microsoft.EntityFrameworkCore;

namespace BaşarSoftDeneme.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Point> Points { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("name=ConnectionStrings:DefaultConnection");
            }
        }

    }
}
