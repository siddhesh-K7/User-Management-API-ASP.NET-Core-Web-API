using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Models;

namespace UserManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Adding seed data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", Age = 30, City = "New York", CreatedAt = DateTime.UtcNow },
                new User { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "0987654321", Age = 25, City = "Los Angeles", CreatedAt = DateTime.UtcNow },
                new User { Id = 3, FirstName = "Alice", LastName = "Johnson", Email = "alice.j@example.com", PhoneNumber = "5551234567", Age = 28, City = "Chicago", CreatedAt = DateTime.UtcNow },
                new User { Id = 4, FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", PhoneNumber = "5559876543", Age = 35, City = "Houston", CreatedAt = DateTime.UtcNow },
                new User { Id = 5, FirstName = "Charlie", LastName = "Davis", Email = "charlie.d@example.com", PhoneNumber = "5554567890", Age = 40, City = "Phoenix", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
