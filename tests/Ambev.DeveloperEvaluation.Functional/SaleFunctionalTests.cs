using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Tests;

/// <summary>
/// Functional tests for the Sale endpoints.
/// </summary>
public class SaleFunctionalTests : IClassFixture<TestApplicationFactory>
{
    private readonly HttpClient _client;

    public SaleFunctionalTests(TestApplicationFactory factory)
    {
        _client = factory.CreateTestClient();
    }
    
    /// <summary>
    /// Tests the CreateSale endpoint with valid data.
    /// </summary>
    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task CreateSale_ValidData_ReturnsSuccess()
    {
        // Arrange
        var command = new CreateSaleCommand(cartId: 5, branchId: 1);

        // Act
        var response = await _client.PostAsJsonAsync("api/sales", command);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        
        var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
        
        Assert.NotNull(result);
        Assert.NotEqual(0, result?.Data?.Id);
        Assert.Contains("Sale created successfully", result?.Message);
    }
}
