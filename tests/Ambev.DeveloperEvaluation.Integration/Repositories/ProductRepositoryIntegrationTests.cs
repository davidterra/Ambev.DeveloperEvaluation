using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Integration.Repositories.TestData;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories;

/// <summary>
/// Contains integration tests for the <see cref="Product"/> entity.
/// </summary>
public class ProductRepositoryIntegrationTests : RepositoryIntegrationTests<TestDbContext, Product>
{
    private readonly DbContextOptions<TestDbContext> _dbContextOptions;

    public ProductRepositoryIntegrationTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase("TestDatabase_Product")
            .Options;
    }

    protected override TestDbContext CreateDbContext()
    {
        return new TestDbContext(_dbContextOptions);
    }

    protected override DbSet<Product> GetDbSet(TestDbContext context)
    {
        return context.Products;
    }

    protected override Product CreateEntity()
    {
        return ProductTestData.GenerateValidEntity();
    }

    protected override object GetEntityKey(Product entity)
    {
        return entity.Id;
    }

    /// <summary>
    /// Additional test to validate filtering by category.
    /// </summary>
    [Fact(DisplayName = "Given products When filtering by category Then returns correct results")]
    public async Task FilterByCategory_ReturnsCorrectResults()
    {
        // Given
        using var context = CreateDbContext();
        var dbSet = GetDbSet(context);

        var product1 = CreateEntity();
        product1.Title = "Laptop";
        product1.Category = "Electronics";

        var product2 = CreateEntity();
        product2.Category = "Another Category";

        dbSet.AddRange(
            product1,
            product2
        );

        await context.SaveChangesAsync();

        // When
        var result = await dbSet.Where(p => p.Category == "Electronics").ToListAsync();

        // Then
        Assert.Single(result);
        Assert.Equal("Laptop", result.First().Title);
    }
}
