using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories;

/// <summary>
/// Contains integration tests for the <see cref="SaleRepository"/> class.
/// </summary>
public class SaleRepositoryIntegrationTests : RepositoryIntegrationTests<TestDbContext, Sale>
{
    private readonly DbContextOptions<TestDbContext> _dbContextOptions;

    public SaleRepositoryIntegrationTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase("TestDatabase_Sale")
            .Options;
    }

    protected override TestDbContext CreateDbContext()
    {
        return new TestDbContext(_dbContextOptions);
    }

    protected override DbSet<Sale> GetDbSet(TestDbContext context)
    {
        return context.Sales;
    }

    protected override Sale CreateEntity()
    {
        return new Sale
        {
            UserId = Guid.NewGuid(),
            BranchId = 1,
            Number = "SALE123",
            CreatedAt = DateTime.UtcNow,
            Items = new List<SaleItem>
            {
                new SaleItem { ProductId = 1, Quantity = 2, UnitPrice = new MonetaryValue(100) }
            }
        };
    }

    protected override object GetEntityKey(Sale entity)
    {
        return entity.Id;
    }

    /// <summary>
    /// Additional test to validate retrieval of a sale by ID.
    /// </summary>
    [Fact(DisplayName = "Given sales When retrieving by ID Then returns correct sale")]
    public async Task GetByIdAsync_ReturnsCorrectSale()
    {
        // Given
        using var context = CreateDbContext();
        var dbSet = GetDbSet(context);

        var sale = CreateEntity();
        context.Sales.Add(sale);
        await context.SaveChangesAsync();

        // When
        var result = await dbSet.Where(s => s.Id == sale.Id).FirstOrDefaultAsync();

        // Then
        Assert.NotNull(result);
        Assert.Equal(sale.Id, result.Id);
        Assert.Equal(sale.Number, result.Number);
        Assert.Equal(sale.Items.Count, result.Items.Count);
    }

    /// <summary>
    /// Additional test to validate updating a sale.
    /// </summary>
    [Fact(DisplayName = "Given a sale When updating Then updates successfully")]
    public async Task UpdateAsync_UpdatesSaleSuccessfully()
    {
        // Given
        using var context = CreateDbContext();
        var dbSet = GetDbSet(context);

        var sale = CreateEntity();
        context.Sales.Add(sale);
        await context.SaveChangesAsync();

        sale.Number = "UPDATED_SALE";
        sale.BranchId = 2;

        // When
        dbSet.Update(sale);
        await context.SaveChangesAsync();
        var updatedSale = await dbSet.FindAsync(sale.Id);

        // Then
        Assert.NotNull(updatedSale);
        Assert.Equal("UPDATED_SALE", updatedSale.Number);
        Assert.Equal(2, updatedSale.BranchId);
    }

    /// <summary>
    /// Additional test to validate creating a sale.
    /// </summary>
    [Fact(DisplayName = "Given a new sale When creating Then creates successfully")]
    public async Task CreateAsync_CreatesSaleSuccessfully()
    {
        // Given
        using var context = CreateDbContext();
        var dbSet = GetDbSet(context);

        var sale = CreateEntity();

        // When
        dbSet.Add(sale);
        await context.SaveChangesAsync();

        var createdSale = await dbSet.FindAsync(sale.Id);

        // Then
        Assert.NotNull(createdSale);
        Assert.NotEqual(0, createdSale.Id);
        Assert.Equal(sale.Number, createdSale.Number);
    }
}

