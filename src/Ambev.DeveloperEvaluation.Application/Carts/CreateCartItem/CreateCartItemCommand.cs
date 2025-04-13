using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCartItem;

/// <summary>
/// Command to create a cart item, containing user, branch, and product details.
/// Implements IRequest to return a CreateCartItemResult.
/// </summary>
public class CreateCartItemCommand : IRequest<CreateCartItemResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid UserId { get; set; }


    /// <summary>
    /// Gets or sets the product details for the cart item.
    /// </summary>
    public CreateCartItemProductCommand Product { get; set; } = null!;

    /// <summary>
    /// Validates the command using the CreateCartItemCommandValidator.
    /// Returns a ValidationResultDetail containing validation status and errors, if any.
    /// </summary>
    /// <returns>A ValidationResultDetail object with validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateCartItemCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Represents the product details for a cart item, including product ID and quantity.
/// </summary>
public class CreateCartItemProductCommand
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product to be added to the cart.
    /// </summary>
    public int Quantity { get; set; }
}
