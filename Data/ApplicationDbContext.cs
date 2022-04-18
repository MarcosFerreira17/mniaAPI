using Microsoft.EntityFrameworkCore;
using mniaAPI.Models;

namespace mniaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Budget>()
            //     .HasOne(b => b.OrderOfService)
            //     .WithOne(i => i.Budget)
            //     .HasForeignKey<OrderOfService>(b => b.BudgetId);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }

    }
}