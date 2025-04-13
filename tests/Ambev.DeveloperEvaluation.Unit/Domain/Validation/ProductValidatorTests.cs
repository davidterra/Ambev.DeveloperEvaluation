using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the ProductValidator class.
/// Tests cover validation of all product properties including title, price, description, category, image, and rating.
/// </summary>
public class ProductValidatorTests
{
    private readonly ProductValidator _validator;

    public ProductValidatorTests()
    {
        _validator = new ProductValidator();
    }

    [Fact(DisplayName = "Valid product should pass all validation rules")]
    public void Given_ValidProduct_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var product = new Product
        {
            Title = "Valid Product",
            Price = new MonetaryValue(100),
            Description = "This is a valid product description.",
            Category = "Electronics",
            Image = "https://example.com/image.jpg",
            Rating = new RatingValue(4.5m, 100)
        };

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory(DisplayName = "Invalid title formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("ab")] // Less than 3 characters
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // More than 100 characters
    public void Given_InvalidTitle_When_Validated_Then_ShouldHaveError(string title)
    {
        // Arrange
        var product = new Product { Title = title };

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact(DisplayName = "Null price should fail validation")]
    public void Given_NullPrice_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = new Product { Price = null };

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact(DisplayName = "Invalid description formats should fail validation")]
    public void Given_InvalidDescription_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = new Product { Description = "abc" }; // Less than 5 characters

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact(DisplayName = "Invalid category formats should fail validation")]
    public void Given_InvalidCategory_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var product = new Product { Category = "" }; // Empty

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Category);
    }

    [Theory(DisplayName = "Invalid image formats should fail validation")]
    [InlineData("")] // Empty
    [InlineData("invalid-url")] // Not a valid URL
    [InlineData("https://example.com/aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")] // Exceeds 500 characters
    public void Given_InvalidImage_When_Validated_Then_ShouldHaveError(string image)
    {
        // Arrange
        var product = new Product { Image = image };

        // Act
        var result = _validator.TestValidate(product);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Image);
    }
    
}
