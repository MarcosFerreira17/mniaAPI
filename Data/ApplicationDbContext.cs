using Microsoft.EntityFrameworkCore;
using mniaAPI.Models;

namespace mniaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }
    }
}