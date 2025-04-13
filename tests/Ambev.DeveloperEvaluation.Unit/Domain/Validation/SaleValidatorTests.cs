using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the SaleValidator class.
/// Tests cover validation of all sale properties, including UserId, BranchId, Items, and TotalAmount.
/// </summary>
public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Valid sale should pass all validation rules")]
    public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.NewGuid(),
            BranchId = 1,
            Items = new List<SaleItem>
            {
                new SaleItem { Id = 1, Quantity = 2, UnitPrice = new MonetaryValue(100) }
            },
            TotalAmount = new MonetaryValue(200)
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when UserId is empty or Guid.Empty.
    /// </summary>
    [Fact(DisplayName = "Empty or Guid.Empty UserId should fail validation")]
    public void Given_InvalidUserId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.Empty,
            BranchId = 1,
            Items = new List<SaleItem>
            {
                new SaleItem { Id = 1, Quantity = 2, UnitPrice = new MonetaryValue(100) }
            },
            TotalAmount = new MonetaryValue(200)
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    /// <summary>
    /// Tests that validation fails when BranchId is less than or equal to zero.
    /// </summary>
    [Fact(DisplayName = "BranchId less than or equal to zero should fail validation")]
    public void Given_InvalidBranchId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.NewGuid(),
            BranchId = 0, // Invalid BranchId
            Items = new List<SaleItem>
            {
                new SaleItem { Id = 1, Quantity = 2, UnitPrice = new MonetaryValue(100) }
            },
            TotalAmount = new MonetaryValue(200)
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BranchId);
    }

    /// <summary>
    /// Tests that validation fails when Items is null or contains items with Quantity <= 0.
    /// </summary>
    [Fact(DisplayName = "Null or invalid Items should fail validation")]
    public void Given_InvalidItems_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.NewGuid(),
            BranchId = 1,
            Items = new List<SaleItem>
            {
                new SaleItem { Id = 1, Quantity = 0, UnitPrice = new MonetaryValue(100) } // Invalid Quantity
            },
            TotalAmount = new MonetaryValue(200)
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    /// <summary>
    /// Tests that validation fails when TotalAmount is null or zero.
    /// </summary>
    [Fact(DisplayName = "Null or zero TotalAmount should fail validation")]
    public void Given_InvalidTotalAmount_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.NewGuid(),
            BranchId = 1,
            Items = new List<SaleItem>
            {
                new SaleItem { Id = 1, Quantity = 2, UnitPrice = new MonetaryValue(100) }
            },
            TotalAmount = null // Invalid TotalAmount
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.TotalAmount);
    }
}
