using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the <see cref="Cart"/> entity.
/// </summary>
public class CartTests
{
    /// <summary>
    /// Tests that a new cart is initialized with default values.
    /// </summary>
    [Fact(DisplayName = "Given new cart When initialized Then sets default values")]
    public void Initialize_NewCart_SetsDefaultValues()
    {
        // Given
        var cart = new Cart();

        // Then
        cart.Id.Should().Be(0);
        cart.UserId.Should().Be(Guid.Empty);
        cart.Status.Should().Be(CartStatus.Active);
        cart.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        cart.UpdatedAt.Should().BeNull();
        cart.CanceledAt.Should().BeNull();
        cart.Items.Should().BeNull();
    }

    /// <summary>
    /// Tests that the cart properties can be set and retrieved correctly.
    /// </summary>
    [Fact(DisplayName = "Given cart When setting properties Then retrieves correct values")]
    public void SetProperties_Cart_SetsAndRetrievesCorrectValues()
    {
        // Given
        var product = new CartItem
        {
            Id = 1,
            ProductId = 1,
            Quantity = 2,
            UnitPrice = new MonetaryValue(100m),
        };

        product.ApplyDiscount(10);

        var products = new List<CartItem>()
        {
            product
        };

        var cart = new Cart
        {
            Id = 1,
            UserId = Guid.NewGuid(),
            Status = CartStatus.Converted,
            CreatedAt = new DateTime(2025, 1, 1),
            UpdatedAt = new DateTime(2025, 1, 2),
            CanceledAt = new DateTime(2025, 1, 3),
            Items = products
        };

        // Then
        cart.Id.Should().Be(1);
        cart.Status.Should().Be(CartStatus.Converted);
        cart.CreatedAt.Should().Be(new DateTime(2025, 1, 1));
        cart.UpdatedAt.Should().Be(new DateTime(2025, 1, 2));
        cart.CanceledAt.Should().Be(new DateTime(2025, 1, 3));
        cart.Items.Should().BeEquivalentTo(products);
    }

    /// <summary>
    /// Tests that the cart's `Deleted` method updates the status and timestamps correctly.
    /// </summary>
    [Fact(DisplayName = "Given cart When deleted Then updates status and timestamps")]
    public void Delete_Cart_UpdatesStatusAndTimestamps()
    {
        // Given
        var cart = new Cart
        {
            Status = CartStatus.Active,
            UpdatedAt = null,
            CanceledAt = null
        };

        // When
        cart.SetAsCanceled();

        // Then
        cart.Status.Should().Be(CartStatus.Canceled);
        cart.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        cart.CanceledAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
}