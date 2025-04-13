using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCartItem;

/// <summary>
/// Validator for DeleteCartItemRequest.
/// Ensures that the request model for deleting a cart item meets the required validation rules.
/// </summary>
public class DeleteCartItemRequestValidator : AbstractValidator<DeleteCartItemRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteCartItemRequest.
    /// Validates that the Id property is greater than zero.
    /// </summary>
    public DeleteCartItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than zero.");
    }
}
