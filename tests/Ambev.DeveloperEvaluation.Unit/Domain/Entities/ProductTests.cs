using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the <see cref="Product"/> entity.
/// </summary>
public class ProductTests
{
    /// <summary>
    /// Tests that a new product is initialized with default values.
    /// </summary>
    [Fact(DisplayName = "Given new product When initialized Then sets default values")]
    public void Initialize_NewProduct_SetsDefaultValues()
    {
        // Given
        var product = new Product();

        // Then
        product.Id.Should().Be(0);
        product.Title.Should().BeEmpty();
        product.Price.Should().Be(MonetaryValue.Zero);
        product.Description.Should().BeEmpty();
        product.Category.Should().BeEmpty();
        product.Image.Should().BeEmpty();
        product.Rating.Should().Be(RatingValue.Zero);
        product.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        product.UpdatedAt.Should().BeNull();        
    }

    /// <summary>
    /// Tests that a valid product passes validation.
    /// </summary>
    [Fact(DisplayName = "Given valid product When validating Then returns valid result")]
    public void Validate_ValidProduct_ReturnsValidResult()
    {
        // Given
        var product = new Product
        {
            Id = 1,
            Title = "Sample Product",
            Price = new MonetaryValue(99.99m),
            Description = "A sample product description",
            Category = "Electronics",
            Image = "https://example.com/image.jpg",
            Rating = new RatingValue(4.5m, 10),
            CreatedAt = DateTime.UtcNow
        };

        // When
        var result = product.Validate();

        // Then
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    /// <summary>
    /// Tests that an invalid product fails validation.
    /// </summary>
    [Fact(DisplayName = "Given invalid product When validating Then returns invalid result")]
    public void Validate_InvalidProduct_ReturnsInvalidResult()
    {
        // Given
        var product = new Product
        {
            Id = 1,
            Title = "", // Invalid: Title is required            
            Description = "A sample product description",
            Category = "Electronics",
            Image = "invalid-url", // Invalid: Image URL is not valid                        
        };

        // When
        var result = product.Validate();

        // Then
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}



