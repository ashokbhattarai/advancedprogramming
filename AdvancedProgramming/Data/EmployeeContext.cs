using AdvancedProgramming.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedProgramming.Data
{
    public class EmployeeContext: DbContext
    {

        public EmployeeContext(DbContextOptions<EmployeeContext> options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>().ToTable("Employee");
        }

    }
}
