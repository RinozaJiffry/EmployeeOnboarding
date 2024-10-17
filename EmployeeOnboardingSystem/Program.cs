using EmployeeOnboarding.Application.UseCases;
using EmployeeOnboarding.Core.Interfaces;
using EmployeeOnboarding.Infrastructure.Data;
using EmployeeOnboarding.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registers Swagger for API documentation
builder.Services.AddSwaggerGen();

// Registers the IEmployeeRepository interface and binds it to the EmployeeRepository implementation.
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Registers the EmployeeOnboardingUseCase to be injected wherever it's needed.
builder.Services.AddScoped<EmployeeOnboardingUseCase>();

// Registers controllers in the application for handling API routes and requests.
builder.Services.AddControllers();

// Registers the EmployeeDbContext for database operations with SQL Server.
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware for enforcing HTTPS in requests (redirects HTTP to HTTPS).
app.UseHttpsRedirection();

// Middleware for handling authorization.
app.UseAuthorization();

// Enable Swagger middleware to expose API documentation.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    c.DocumentTitle = "Employee Onboarding API";
});

// Maps the controllers to handle incoming HTTP requests, based on the routes defined in the controllers.
app.MapControllers();

// Runs the application and starts listening for HTTP requests.
app.Run();
