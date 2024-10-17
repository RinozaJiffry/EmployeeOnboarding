using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeOnboarding.Application.DTOs
{
    public class EmployeeDTO
    {
        // Property to store the employee's first name. 
        public string FirstName { get; set; }

        // Property to store the employee's last name.
        public string LastName { get; set; }

        // Property to store the employee's email address.
        public string Email { get; set; }

        // Property to store the date the employee joined the company.
        public DateTime DateOfJoining { get; set; }
    }
}
