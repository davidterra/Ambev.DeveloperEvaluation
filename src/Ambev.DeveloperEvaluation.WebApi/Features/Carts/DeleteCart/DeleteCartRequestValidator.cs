using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

/// <summary>
/// Validator for DeleteCartRequest
/// </summary>
public class DeleteCartRequestValidator : AbstractValidator<DeleteCartRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteCartRequest.
    /// Ensures that the Id property is valid and meets the required conditions.
    /// </summary>
    public DeleteCartRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than zero.");
    }
}
