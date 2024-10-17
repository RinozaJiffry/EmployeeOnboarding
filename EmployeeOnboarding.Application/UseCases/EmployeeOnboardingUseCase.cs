using EmployeeOnboarding.Application.DTOs;
using EmployeeOnboarding.Core.Entities;
using EmployeeOnboarding.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeOnboarding.Application.UseCases
{
    public class EmployeeOnboardingUseCase
    {
        // A private field that stores a reference to the IEmployeeRepository.
 
        private readonly IEmployeeRepository _repository;

        // Constructor that injects the IEmployeeRepository dependency through dependency injection.
        // The repository is needed to handle the addition of a new employee.
        public EmployeeOnboardingUseCase(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // Asynchronously handles the onboarding process for a new employee.
        public async Task OnboardEmployeeAsync(EmployeeDTO employeeDto)
        {
            // Validation: Ensure FirstName, LastName, and Email are not empty
            if (string.IsNullOrWhiteSpace(employeeDto.FirstName))
            {
                throw new ArgumentException("First Name is required");
            }

            if (string.IsNullOrWhiteSpace(employeeDto.LastName))
            {
                throw new ArgumentException("Last Name is required");
            }

            if (string.IsNullOrWhiteSpace(employeeDto.Email))
            {
                throw new ArgumentException("Email is required");
            }

            // Validation: Ensure the email is in a valid format using Regex
            if (!IsValidEmail(employeeDto.Email))
            {
                throw new ArgumentException("Invalid email format");
            }

            // Check for duplicate employee (same first name, last name, and email)
            bool isDuplicate = await _repository.IsDuplicateEmployeeAsync(employeeDto.FirstName, employeeDto.LastName, employeeDto.Email);
            if (isDuplicate)
            {
                throw new ArgumentException("This employee is already onboarded to the system");
            }

            // Creates a new Employee object and maps the properties from the EmployeeDTO.
            var employee = new Employee
            {
                // Generate a new Guid for the employee Id
                Id = Guid.NewGuid(),

                // Assigns the first name from DTO to the Employee entity.
                FirstName = employeeDto.FirstName,

                // Assigns the last name from DTO to the Employee entity.
                LastName = employeeDto.LastName,

                // Assigns the email from DTO to the Employee entity.
                Email = employeeDto.Email,

                // Assigns the joining date from DTO to the Employee entity.
                DateOfJoining = employeeDto.DateOfJoining  
            };

            // Calls the repository to add the new employee to the data store asynchronously.
            await _repository.AddEmployeeAsync(employee);
        }

        // Utility method to validate email format using Regex.
        private bool IsValidEmail(string email)
        {
            // Define a regular expression for validating email format
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Use Regex to check if the email matches the pattern
            return Regex.IsMatch(email, emailRegex);
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await _repository.GetEmployeeByIdAsync(id);
        }

        // Method to delete an employee by ID
        public async Task<string> DeleteEmployeeByIdAsync(Guid id)
        {
            // Retrieve the employee details before deletion
            var employee = await _repository.GetEmployeeByIdAsync(id);

            // Check if the employee exists
            if (employee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            // Call the repository to delete the employee
            await _repository.DeleteEmployeeAsync(id);

            // Return a descriptive message including the employee's ID, FirstName, and LastName
            return $"Employee with GUID {employee.Id} (First Name: {employee.FirstName}, Last Name: {employee.LastName}) was successfully deleted from the system.";
        }    

    }
}
