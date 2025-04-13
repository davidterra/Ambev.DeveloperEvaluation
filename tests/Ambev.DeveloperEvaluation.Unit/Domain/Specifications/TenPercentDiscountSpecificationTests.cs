using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications.Discounts;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

/// <summary>
/// Contains unit tests for the <see cref="TenPercentDiscountSpecification"/> class.
/// </summary>
public class TenPercentDiscountSpecificationTests
{
    private readonly TenPercentDiscountSpecification _specification;

    /// <summary>
    /// Initializes a new instance of the <see cref="TenPercentDiscountSpecificationTests"/> class.
    /// </summary>
    public TenPercentDiscountSpecificationTests()
    {
        _specification = new TenPercentDiscountSpecification();
    }

    /// <summary>
    /// Tests that the specification is satisfied when the product quantity is between 4 and 9 (inclusive).
    /// </summary>
    [Theory(DisplayName = "Given product with quantity between 4 and 9 When checking specification Then is satisfied")]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(9)]
    public void IsSatisfiedBy_QuantityBetween4And9_ReturnsTrue(int quantity)
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = quantity
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeTrue();
    }

    /// <summary>
    /// Tests that the specification is not satisfied when the product quantity is less than 4.
    /// </summary>
    [Fact(DisplayName = "Given product with quantity less than 4 When checking specification Then is not satisfied")]
    public void IsSatisfiedBy_QuantityLessThan4_ReturnsFalse()
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = 3
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeFalse();
    }

    /// <summary>
    /// Tests that the specification is not satisfied when the product quantity is 10 or more.
    /// </summary>
    [Fact(DisplayName = "Given product with quantity 10 or more When checking specification Then is not satisfied")]
    public void IsSatisfiedBy_Quantity10OrMore_ReturnsFalse()
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = 10
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeFalse();
    }
}


