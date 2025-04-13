using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications.Discounts;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

/// <summary>
/// Contains unit tests for the <see cref="QuantityLimitSpecification"/> class.
/// </summary>
public class QuantityLimitSpecificationTests
{
    private readonly QuantityLimitSpecification _specification;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuantityLimitSpecificationTests"/> class.
    /// </summary>
    public QuantityLimitSpecificationTests()
    {
        _specification = new QuantityLimitSpecification();
    }

    /// <summary>
    /// Tests that the specification is satisfied when the product quantity is greater than 20.
    /// </summary>
    [Fact(DisplayName = "Given product with quantity greater than 20 When checking specification Then is satisfied")]
    public void IsSatisfiedBy_QuantityGreaterThan20_ReturnsTrue()
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = 21
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeTrue();
    }

    /// <summary>
    /// Tests that the specification is not satisfied when the product quantity is equal to 20.
    /// </summary>
    [Fact(DisplayName = "Given product with quantity equal to 20 When checking specification Then is not satisfied")]
    public void IsSatisfiedBy_QuantityEqualTo20_ReturnsFalse()
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = 20
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeFalse();
    }

    /// <summary>
    /// Tests that the specification is not satisfied when the product quantity is less than 20.
    /// </summary>
    [Fact(DisplayName = "Given product with quantity less than 20 When checking specification Then is not satisfied")]
    public void IsSatisfiedBy_QuantityLessThan20_ReturnsFalse()
    {
        // Given
        var cartProduct = new CartItem
        {
            Quantity = 19
        };

        // When
        var result = _specification.IsSatisfiedBy(cartProduct);

        // Then
        result.Should().BeFalse();
    }
}

