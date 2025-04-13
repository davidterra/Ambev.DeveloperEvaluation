using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SaleItemValidator class.
/// Tests cover validation of all SaleItem properties, including ProductId, Quantity, and UnitPrice.
/// </summary>
public class SaleItemValidatorTests
{
    private readonly SaleItemValidator _validator;

    public SaleItemValidatorTests()
    {
        _validator = new SaleItemValidator();
    }

    /// <summary>
    /// Tests that validation passes when all SaleItem properties are valid.
    /// </summary>
    [Fact(DisplayName = "Valid SaleItem should pass all validation rules")]
    public void Given_ValidSaleItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductId = 1,
            Quantity = 2,
            UnitPrice = new MonetaryValue(100)
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when ProductId is less than or equal to zero.
    /// </summary>
    [Fact(DisplayName = "ProductId less than or equal to zero should fail validation")]
    public void Given_InvalidProductId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductId = 0, // Invalid ProductId
            Quantity = 2,
            UnitPrice = new MonetaryValue(100)
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }

    /// <summary>
    /// Tests that validation fails when Quantity is less than or equal to zero.
    /// </summary>
    [Fact(DisplayName = "Quantity less than or equal to zero should fail validation")]
    public void Given_InvalidQuantity_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductId = 1,
            Quantity = 0, // Invalid Quantity
            UnitPrice = new MonetaryValue(100)
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    /// <summary>
    /// Tests that validation fails when UnitPrice is less than or equal to zero.
    /// </summary>
    [Fact(DisplayName = "UnitPrice less than or equal to zero should fail validation")]
    public void Given_InvalidUnitPrice_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            ProductId = 1,
            Quantity = 2,
            UnitPrice = new MonetaryValue(0) // Invalid UnitPrice
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice.Amount);
    }
}
