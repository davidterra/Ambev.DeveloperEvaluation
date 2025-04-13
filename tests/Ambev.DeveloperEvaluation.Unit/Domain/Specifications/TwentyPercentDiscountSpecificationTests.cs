using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications.Discounts;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

/// <summary>
/// Contains unit tests for the <see cref="TwentyPercentDiscountSpecification"/> class.
/// </summary>
public class TwentyPercentDiscountSpecificationTests
{
    private readonly TwentyPercentDiscountSpecification _specification;

    /// <summary>
    /// Initializes a new instance of the <see cref="TwentyPercentDiscountSpecificationTests"/> class.
    /// </summary>
    public TwentyPercentDiscountSpecificationTests()
    {
        _specification = new TwentyPercentDiscountSpecification();
    }

    /// <summary>
    /// Tests that the specification is satisfied when the product quantity is between 10 and 20 (inclusive).
    /// </summary>
    [Theory(DisplayName = "Given product with quantity between 10 and 20 When checking specification Then is satisfied")]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    public void IsSatisfiedBy_QuantityBetween10And20_ReturnsTrue(int quantity)
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
    /// Tests that the specification is not satisfied when the product quantity is less than 10.
    /// </summary>
    [Fact(DisplayName = "Given product with quantity less than 10 When checking specification Then is not satisfied")]
    public void IsSatisfiedBy_QuantityLessThan10_ReturnsFalse()
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = 9
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeFalse();
    }

    /// <summary>
    /// Tests that the specification is not satisfied when the product quantity is greater than 20.
    /// </summary>
    [Fact(DisplayName = "Given product with quantity greater than 20 When checking specification Then is not satisfied")]
    public void IsSatisfiedBy_QuantityGreaterThan20_ReturnsFalse()
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = 21
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeFalse();
    }
}