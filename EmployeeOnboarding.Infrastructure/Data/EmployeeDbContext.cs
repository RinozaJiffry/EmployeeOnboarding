using EmployeeOnboarding.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeOnboarding.Infrastructure.Data
{
    // This class represents the database context used by Entity Framework Core.
    public class EmployeeDbContext : DbContext
    {
        // Constructor to pass DbContextOptions to the base DbContext class.
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        // This property allow to query and save instances of Employee.
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Employee entity to have a primary key.
            modelBuilder.Entity<Employee>()

                // Ensure 'Id' is set as the primary key.
                .HasKey(e => e.Id); 
        }

    }
}