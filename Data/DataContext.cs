using Microsoft.EntityFrameworkCore;
using PetrolStationNetwork.Models;

namespace PetrolStationNetwork.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users  { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Staff> Staff { get; set; }

        public DataContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Config.connection, Config.version);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");

            // Настрока связи один-к-одному между User и Supplier
            modelBuilder.Entity<Supplier>()
                .ToTable("Suppliers")
                .HasKey(s => s.user_id);

            modelBuilder.Entity<Supplier>()
                .HasOne(s => s.User)
                .WithOne(u => u.Supplier)
                .HasForeignKey<Supplier>(s => s.user_id)
                .OnDelete(DeleteBehavior.Cascade);

            // Настрока связи один-к-одному между User и Staff
            modelBuilder.Entity<Staff>()
                .ToTable("Staff")
                .HasKey(s => s.user_id);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.User)
                .WithOne(u => u.Staff)
                .HasForeignKey<Staff>(s => s.user_id)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для свойства Role в Staff
            modelBuilder.Entity<Staff>()
                .Property(s => s.Role)
                .HasConversion<string>()
                .HasColumnName("role");

            base.OnModelCreating(modelBuilder);
        }
    }
}
