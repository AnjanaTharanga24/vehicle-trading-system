using Microsoft.EntityFrameworkCore;
using VehicleTradingSystem.Models;

namespace VehicleTradingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=vehicle_trading_db;user=root;password=1234",
                    new MySqlServerVersion(new Version(8, 0, 23))); // Use your MySQL version
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerID);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.NIC).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.MobilePhone).IsRequired();
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });
        }
    }
}