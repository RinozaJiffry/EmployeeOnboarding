using EmployeeOnboarding.Core.Entities;
using EmployeeOnboarding.Core.Interfaces;
using EmployeeOnboarding.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeOnboarding.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // Private field to store the instance of the database context (EmployeeDbContext).
        private readonly EmployeeDbContext _context;

        // Constructor that injects the database context using dependency injection.
        public EmployeeRepository(EmployeeDbContext context)
        {
            // Assigns the injected context to the private field.
            _context = context; 
        }

        // Check for duplicate employees by FirstName, LastName, and Email
        public async Task<bool> IsDuplicateEmployeeAsync(string firstName, string lastName, string email)
        {
            // Query the database to see if an employee with the same firstName, lastName, and email already exists
            return await _context.Employees.AnyAsync(e =>
                e.FirstName == firstName &&
                e.LastName == lastName &&
                e.Email == email);
        }


        // Asynchronously retrieves all employees from the database.
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            // Queries the Employees DbSet and converts the result into a list asynchronously.
            return await _context.Employees.ToListAsync();
        }

        // Asynchronously retrieves a single employee by their ID from the database.
        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            // Uses FindAsync to locate the employee by their primary key (Id) in the database.
            return await _context.Employees.FindAsync(id);
        }

        // Asynchronously adds a new employee to the database.
        public async Task AddEmployeeAsync(Employee employee)
        {

            // Ensure a new GUID is assigned before saving
            employee.Id = Guid.NewGuid(); 

            // Adds the provided employee to the Employees DbSet (in-memory).
            _context.Employees.Add(employee);

            // Saves changes to the database asynchronously to persist the new employee.
            await _context.SaveChangesAsync();  // This ensures that the new data is saved in the database.
        }

        // Asynchronously deletes an employee by ID
        public async Task DeleteEmployeeAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}