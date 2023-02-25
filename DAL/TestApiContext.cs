using DAL.Migrations;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TestApiContext : DbContext
    {
        public TestApiContext(DbContextOptions<TestApiContext> options)
            :base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Employee>().ToTable("Employee");

            modelBuilder.Entity<Employee>()
                         .HasIndex(u => u.Email)
                        .IsUnique();



        }       

    }
}