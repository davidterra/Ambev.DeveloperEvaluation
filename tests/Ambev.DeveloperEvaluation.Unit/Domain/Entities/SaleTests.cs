using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover status changes, subtotal calculations, and validation scenarios.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that when a sale is canceled, its status is updated correctly.
    /// </summary>
    [Fact(DisplayName = "Sale should be marked as canceled when SetAsCanceled is called")]
    public void Given_Sale_When_SetAsCanceled_Then_ShouldUpdateCanceledAtAndUpdatedAt()
    {
        // Arrange
        var sale = new Sale
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow,
            Items = new List<SaleItem>
            {
                new SaleItem { Id = 1, Quantity = 2, UnitPrice = new MonetaryValue(100) }
            }
        };

        // Act
        sale.SetAsCanceled();

        // Assert
        Assert.NotNull(sale.CanceledAt);
        Assert.NotNull(sale.UpdatedAt);
        Assert.True(sale.CanceledAt <= DateTime.UtcNow);
        Assert.True(sale.UpdatedAt <= DateTime.UtcNow);
    }

    /// <summary>
    /// Tests that when a sale is updated, its UpdatedAt timestamp is set correctly.
    /// </summary>
    [Fact(DisplayName = "Sale should update UpdatedAt when SetAsUpdated is called")]
    public void Given_Sale_When_SetAsUpdated_Then_ShouldUpdateUpdatedAt()
    {
        // Arrange
        var sale = new Sale
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        sale.SetAsUpdated();

        // Assert
        Assert.NotNull(sale.UpdatedAt);
        Assert.True(sale.UpdatedAt <= DateTime.UtcNow);
    }

    /// <summary>
    /// Tests that the subtotal is calculated correctly for a sale with valid items.
    /// </summary>
    [Fact(DisplayName = "Subtotal should be calculated correctly for valid items")]
    public void Given_SaleWithValidItems_When_SubtotalIsCalled_Then_ShouldCalculateTotalAmount()
    {
        // Arrange
        var sale = new Sale
        {
            Id = 1,
            Items = new List<SaleItem>
            {
                new SaleItem { Id = 1, Quantity = 2, UnitPrice = new MonetaryValue(100), TotalAmount = new MonetaryValue(200) },
                new SaleItem { Id = 2, Quantity = 1, UnitPrice = new MonetaryValue(50), TotalAmount = new MonetaryValue(50) }
            }
        };

        // Act
        sale.Subtotal();

        // Assert
        Assert.Equal(250, sale.TotalAmount.Amount);
    }

    /// <summary>
    /// Tests that the subtotal excludes canceled items.
    /// </summary>
    [Fact(DisplayName = "Subtotal should exclude canceled items")]
    public void Given_SaleWithCanceledItems_When_SubtotalIsCalled_Then_ShouldExcludeCanceledItems()
    {
        // Arrange
        var sale = new Sale
        {
            Id = 1,
            Items = new List<SaleItem>
            {
                new SaleItem { Id = 1, Quantity = 2, UnitPrice = new MonetaryValue(100), TotalAmount = new MonetaryValue(200) },
                new SaleItem { Id = 2, Quantity = 1, UnitPrice = new MonetaryValue(50), TotalAmount = new MonetaryValue(50), CanceledAt = DateTime.UtcNow }
            }
        };

        // Act
        sale.Subtotal();

        // Assert
        Assert.Equal(200, sale.TotalAmount.Amount);
    }
}
