using Microsoft.EntityFrameworkCore;
using VehicleTradingSystem.Models;

namespace VehicleTradingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

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

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.VehicleID);
                entity.Property(e => e.Make).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.VIN).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Color).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Available");
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.DateAdded).IsRequired();
                entity.Property(e => e.FuelType).HasMaxLength(50);
                entity.Property(e => e.Transmission).HasMaxLength(20);

                // Create unique index for VIN
                entity.HasIndex(e => e.VIN).IsUnique();
            });
        }
    }
}