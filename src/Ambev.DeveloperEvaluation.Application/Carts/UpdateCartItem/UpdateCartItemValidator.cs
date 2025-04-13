using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;

/// <summary>
/// Validator for the UpdateCartItemCommand class.
/// Ensures that the Id and Quantity properties meet the required validation rules.
/// </summary>
public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCartItemCommandValidator"/> class.
    /// Defines validation rules for the UpdateCartItemCommand.
    /// </summary>
    public UpdateCartItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than zero.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}
