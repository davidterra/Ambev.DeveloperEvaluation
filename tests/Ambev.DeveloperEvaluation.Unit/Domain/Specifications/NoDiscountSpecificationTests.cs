using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications.Discounts;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

/// <summary>
/// Contains unit tests for the <see cref="NoDiscountSpecification"/> class.
/// </summary>
public class NoDiscountSpecificationTests
{
    private readonly NoDiscountSpecification _specification;

    /// <summary>
    /// Initializes a new instance of the <see cref="NoDiscountSpecificationTests"/> class.
    /// </summary>
    public NoDiscountSpecificationTests()
    {
        _specification = new NoDiscountSpecification();
    }

    /// <summary>
    /// Tests that the specification is satisfied when the product quantity is less than 4 and a discount is applied.
    /// </summary>
    [Fact(DisplayName = "Given product with quantity less than 4 and discount When checking specification Then is satisfied")]
    public void IsSatisfiedBy_QuantityLessThan4WithDiscount_ReturnsTrue()
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = 3,
            DiscountPercent = new PercentageValue(10)
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeTrue();
    }

    /// <summary>
    /// Tests that the specification is not satisfied when the product quantity is 4 or more.
    /// </summary>
    [Fact(DisplayName = "Given product with quantity 4 or more When checking specification Then is not satisfied")]
    public void IsSatisfiedBy_Quantity4OrMore_ReturnsFalse()
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = 4,
            DiscountPercent = new PercentageValue(10)
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeFalse();
    }

    /// <summary>
    /// Tests that the specification is not satisfied when no discount is applied.
    /// </summary>
    [Fact(DisplayName = "Given product with no discount When checking specification Then is not satisfied")]
    public void IsSatisfiedBy_NoDiscount_ReturnsFalse()
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = 3,
            DiscountPercent = PercentageValue.Zero
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeFalse();
    }

    /// <summary>
    /// Tests that the specification is not satisfied when the product quantity is 4 or more and no discount is applied.
    /// </summary>
    [Fact(DisplayName = "Given product with quantity 4 or more and no discount When checking specification Then is not satisfied")]
    public void IsSatisfiedBy_Quantity4OrMoreNoDiscount_ReturnsFalse()
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = 4,
            DiscountPercent = PercentageValue.Zero
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeFalse();
    }
}
