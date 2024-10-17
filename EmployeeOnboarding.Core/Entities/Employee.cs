using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeOnboarding.Core.Entities
{
    public class Employee
    {
        // Property to store the unique identifier of the employee
        public Guid Id { get; set; }

        // Property to store the employee's first name.
        public required string FirstName { get; set; }

        // Property to store the employee's last name.
        public required string LastName { get; set; }

        // Property to store the employee's email address.
        public required string Email { get; set; }

        // Property to store the date the employee joined the company.
        public DateTime DateOfJoining { get; set; }
    }
}
