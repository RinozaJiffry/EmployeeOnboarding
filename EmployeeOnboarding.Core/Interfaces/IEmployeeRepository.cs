using EmployeeOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeOnboarding.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        // Avoid Duplication Record
        Task<bool> IsDuplicateEmployeeAsync(string firstName, string lastName, string email);

        // Asynchronously retrieves all employees as a collection of Employee objects.
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        // Asynchronously retrieves a single employee based on their unique identifier (id).
        Task<Employee> GetEmployeeByIdAsync(Guid id);

        // Asynchronously adds a new employee to the system.
        Task AddEmployeeAsync(Employee employee);

        //Delete the employee record by Id
        Task DeleteEmployeeAsync(Guid id);
    }
}
