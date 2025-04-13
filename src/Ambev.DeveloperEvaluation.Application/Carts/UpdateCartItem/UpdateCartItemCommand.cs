using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;

/// <summary>
/// Command to update a cart item with a specified quantity.
/// Implements IRequest to return an UpdateCartItemResult.
/// </summary>
public class UpdateCartItemCommand : IRequest<UpdateCartItemResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart item to be updated.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the new quantity for the cart item.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Validates the command using the UpdateCartItemCommandValidator.
    /// Returns a ValidationResultDetail containing validation results.
    /// </summary>
    /// <returns>A ValidationResultDetail object with validation status and errors.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateCartItemCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
