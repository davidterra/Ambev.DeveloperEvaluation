using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the <see cref="CartItemValidator"/> class.
/// </summary>
public class CartItemValidatorTests
{
    private readonly CartItemValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CartItemValidatorTests"/> class.
    /// </summary>
    public CartItemValidatorTests()
    {
        _validator = new CartItemValidator();
    }

    /// <summary>
    /// Tests that a valid cart product passes validation.
    /// </summary>
    [Fact(DisplayName = "Given valid cart product When validating Then passes validation")]
    public void Validate_ValidCartProduct_PassesValidation()
    {
        // Given
        var cartProduct = new CartItem
        {
            ProductId = 1,
            Quantity = 2,
            UnitPrice = new MonetaryValue(99.99m)
        };

        // When
        var result = _validator.TestValidate(cartProduct);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that a cart product with an invalid product ID fails validation.
    /// </summary>
    [Fact(DisplayName = "Given invalid product ID When validating Then fails validation")]
    public void Validate_InvalidProductId_FailsValidation()
    {
        // Given
        var cartProduct = new CartItem
        {
            ProductId = 0, // Invalid: Product ID must be greater than 0
            Quantity = 2,
            UnitPrice = new MonetaryValue(99.99m)
        };

        // When
        var result = _validator.TestValidate(cartProduct);

        // Then
        result.ShouldHaveValidationErrorFor(cp => cp.ProductId)
            .WithErrorMessage("Product ID is required.");
    }

    /// <summary>
    /// Tests that a cart product with an invalid quantity fails validation.
    /// </summary>
    [Fact(DisplayName = "Given invalid quantity When validating Then fails validation")]
    public void Validate_InvalidQuantity_FailsValidation()
    {
        // Given
        var cartProduct = new CartItem
        {
            ProductId = 1,
            Quantity = 0, // Invalid: Quantity must be greater than 0
            UnitPrice = new MonetaryValue(99.99m)
        };

        // When
        var result = _validator.TestValidate(cartProduct);

        // Then
        result.ShouldHaveValidationErrorFor(cp => cp.Quantity)
            .WithErrorMessage("Quantity must be greater than zero.");
    }

    /// <summary>
    /// Tests that a cart product with a null unit price fails validation.
    /// </summary>
    [Fact(DisplayName = "Given null unit price When validating Then fails validation")]
    public void Validate_NullUnitPrice_FailsValidation()
    {
        // Given
        var cartProduct = new CartItem
        {
            ProductId = 1,
            Quantity = 2,
            UnitPrice = null! // Invalid: Unit price is required
        };

        // When
        var result = _validator.TestValidate(cartProduct);

        // Then
        result.ShouldHaveValidationErrorFor(cp => cp.UnitPrice)
            .WithErrorMessage("Unit price is required.");
    }

    /// <summary>
    /// Tests that a cart product with a unit price less than or equal to zero fails validation.
    /// </summary>
    [Fact(DisplayName = "Given unit price less than or equal to zero When validating Then fails validation")]
    public void Validate_UnitPriceLessThanOrEqualToZero_FailsValidation()
    {
        // Given
        var cartProduct = new CartItem
        {
            ProductId = 1,
            Quantity = 2,
            UnitPrice = new MonetaryValue(0) // Invalid: Unit price must be greater than zero
        };

        // When
        var result = _validator.TestValidate(cartProduct);

        // Then
        result.ShouldHaveValidationErrorFor(cp => cp.UnitPrice)
            .WithErrorMessage("Unit price must be greater than zero.");
    }
}