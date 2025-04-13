using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;

/// <summary>
/// Validator for DeleteCartItemCommand
/// </summary>
public class DeleteCartItemValidator : AbstractValidator<DeleteCartItemCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteCartItemCommand
    /// </summary>
    public DeleteCartItemValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than zero.");
    }
}
