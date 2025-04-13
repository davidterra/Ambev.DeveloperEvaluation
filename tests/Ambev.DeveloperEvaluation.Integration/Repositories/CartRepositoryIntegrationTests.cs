using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories;

/// <summary>
/// Contains integration tests for the <see cref="Cart"/> repository.
/// </summary>
public class CartRepositoryIntegrationTests : RepositoryIntegrationTests<TestDbContext, Cart>
{
    private readonly DbContextOptions<TestDbContext> _dbContextOptions;

    public CartRepositoryIntegrationTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase("TestDatabase_Cart")
            .Options;
    }

    protected override TestDbContext CreateDbContext()
    {
        return new TestDbContext(_dbContextOptions);
    }

    protected override DbSet<Cart> GetDbSet(TestDbContext context)
    {
        return context.Carts;
    }

    protected override Cart CreateEntity()
    {
        return new Cart
        {
            UserId = Guid.NewGuid(),
            Status = CartStatus.Active,
            CreatedAt = DateTime.UtcNow,
            Items = new List<CartItem>
            {
                new CartItem
                {
                    ProductId = 1,
                    Quantity = 2,
                    UnitPrice = new MonetaryValue(99.99m),
                }
            }
        };
    }

    protected override object GetEntityKey(Cart entity)
    {
        return entity.Id;
    }

    /// <summary>
    /// Additional test to validate filtering by status.
    /// </summary>
    [Fact(DisplayName = "Given carts When filtering by status Then returns correct results")]
    public async Task FilterByStatus_ReturnsCorrectResults()
    {
        // Given
        using var context = CreateDbContext();
        var dbSet = GetDbSet(context);

        var cart1 = CreateEntity();
        cart1.Status = CartStatus.Active;

        var cart2 = CreateEntity();
        cart2.Status = CartStatus.Canceled;

        dbSet.AddRange(cart1, cart2);
        await context.SaveChangesAsync();

        // When
        var result = await dbSet.Where(c => c.Status == CartStatus.Active).ToListAsync();

        // Then
        Assert.Single(result);
        Assert.Equal(CartStatus.Active, result.First().Status);
    }
}
