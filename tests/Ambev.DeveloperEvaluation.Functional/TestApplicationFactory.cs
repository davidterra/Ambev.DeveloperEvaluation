using System.Net.Http;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.Functional.Tests;

/// <summary>
/// Factory for creating a test application environment.
/// </summary>
public class TestApplicationFactory : WebApplicationFactory<Program>
{
    /// <summary>
    /// Configures the test environment for the application.
    /// </summary>
    /// <param name="builder">The web host builder.</param>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration
            //var descriptor = services.SingleOrDefault(
            //    d => d.ServiceType == typeof(DbContextOptions<DefaultContext>));
            //if (descriptor != null)
            //{
            //    services.Remove(descriptor);
            //}

            // Add an in-memory database for testing
            //services.AddDbContext<DefaultContext>(options =>
            //{
            //    options.UseInMemoryDatabase("TestDatabase");
            //});

            // Ensure the database is created
            //var serviceProvider = services.BuildServiceProvider();
            //using var scope = serviceProvider.CreateScope();
            //var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            //dbContext.Database.EnsureCreated();
        });
    }

    /// <summary>
    /// Creates an HTTP client for testing.
    /// </summary>
    /// <returns>An HTTP client configured for the test application.</returns>
    public HttpClient CreateTestClient()
    {
        return CreateClient();
    }
}
