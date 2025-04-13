using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Integration.TestData;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories;

/// <summary>
/// Contains integration tests for the <see cref="User"/> repository.
/// </summary>
public class UserRepositoryIntegrationTests : RepositoryIntegrationTests<TestDbContext, User>
{
    private readonly DbContextOptions<TestDbContext> _dbContextOptions;

    public UserRepositoryIntegrationTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase("TestDatabase_User")
            .Options;
    }

    protected override TestDbContext CreateDbContext()
    {
        return new TestDbContext(_dbContextOptions);
    }

    protected override DbSet<User> GetDbSet(TestDbContext context)
    {
        return context.Users;
    }

    protected override User CreateEntity()
    {
        return UserTestData.GenerateValidEntity();
    }

    protected override object GetEntityKey(User entity)
    {
        return entity.Id;
    }

    /// <summary>
    /// Additional test to validate filtering by status.
    /// </summary>
    [Fact(DisplayName = "Given users When filtering by status Then returns correct results")]
    public async Task FilterByStatus_ReturnsCorrectResults()
    {
        // Given
        using var context = CreateDbContext();
        var dbSet = GetDbSet(context);

        var user1 = CreateEntity();
        user1.Activate();

        var user2 = CreateEntity();
        user2.Deactivate();

        dbSet.AddRange(user1, user2);
        await context.SaveChangesAsync();

        // When
        var result = await dbSet.Where(u => u.Status == UserStatus.Inactive).ToListAsync();

        // Then
        Assert.Single(result);
        Assert.Equal(UserStatus.Inactive, result.First().Status);
    }
}
