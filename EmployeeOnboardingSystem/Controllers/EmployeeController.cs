using EmployeeOnboarding.Application.DTOs;
using EmployeeOnboarding.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeOnboarding.WebAPI.Controllers
{
    // Responsible for handling HTTP requests.
    [ApiController]

    // Defines the route for the controller.
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        // A private field for the use case that handles employee onboarding.
        private readonly EmployeeOnboardingUseCase _useCase;

        // Constructor injection is used to pass the EmployeeOnboardingUseCase dependency.
        public EmployeeController(EmployeeOnboardingUseCase useCase)
        {
            _useCase = useCase;
        }

        // HTTP POST endpoint to onboard a new employee.
        [HttpPost]
        public async Task<IActionResult> OnboardEmployee([FromBody] EmployeeDTO employeeDto)
        {
            try
            {
                // Calls the use case to handle onboarding the employee asynchronously.
                await _useCase.OnboardEmployeeAsync(employeeDto);

                // Returns a success message and HTTP 200 OK response when the employee is onboarded successfully.
                return Ok("Employee onboarded successfully");
            }
            catch (ArgumentException ex)
            {
                // Return a 400 BadRequest with the validation message
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _useCase.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                // Call the use case to delete the employee by ID
                var message = await _useCase.DeleteEmployeeByIdAsync(id);

                // Return the success message in the response
                return Ok(new { message });
            }
            catch (ArgumentException ex)
            {
                // If employee not found, return NotFound status with a message
                return NotFound(new { error = ex.Message });
            }
        }

    }
}

