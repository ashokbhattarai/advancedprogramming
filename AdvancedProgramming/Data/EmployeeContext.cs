using AdvancedProgramming.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedProgramming.Data
{
    public class EmployeeContext: DbContext
    {

        public EmployeeContext(DbContextOptions<EmployeeContext> options): base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }


        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Designation>().ToTable("Designation");
            //All Indexing is not possible from dataannotations

            modelBuilder.Entity<Employee>()
     .HasIndex(u => u.NickName)
     .IsUnique();

        }

    }
}
