using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Integration.Repositories.TestData;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories;

/// <summary>
/// Contains integration tests for the <see cref="Branch"/> repository.
/// </summary>
public class BranchRepositoryIntegrationTests : RepositoryIntegrationTests<TestDbContext, Branch>
{
    private readonly DbContextOptions<TestDbContext> _dbContextOptions;

    public BranchRepositoryIntegrationTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase("TestDatabase_Branch")
            .Options;
    }

    protected override TestDbContext CreateDbContext()
    {
        return new TestDbContext(_dbContextOptions);
    }

    protected override DbSet<Branch> GetDbSet(TestDbContext context)
    {
        return context.Branches;
    }

    protected override Branch CreateEntity()
    {
        return BranchesTestData.GenerateValidEntity();
    }

    protected override object GetEntityKey(Branch entity)
    {
        return entity.Id;
    }

    /// <summary>
    /// Additional test to validate filtering by name.
    /// </summary>
    [Fact(DisplayName = "Given branches When filtering by name Then returns correct results")]
    public async Task FilterByName_ReturnsCorrectResults()
    {
        // Given
        using var context = CreateDbContext();
        var dbSet = GetDbSet(context);

        var branch1 = CreateEntity();
        branch1.Name = "Main Branch";

        var branch2 = CreateEntity();
        branch2.Name = "Secondary Branch";

        dbSet.AddRange(branch1, branch2);
        await context.SaveChangesAsync();

        // When
        var result = await dbSet.Where(b => b.Name == "Main Branch").ToListAsync();

        // Then
        Assert.Single(result);
        Assert.Equal("Main Branch", result.First().Name);
    }
}
