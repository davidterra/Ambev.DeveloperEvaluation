using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Validator for the GetCartCommand, ensuring that the command contains valid data.
/// </summary>
public class GetCartValidator : AbstractValidator<GetCartCommand>
{
    /// <summary>
    /// Initializes validation rules for the GetCartCommand.
    /// Ensures that the Id property is greater than zero.
    /// </summary>
    public GetCartValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("User ID must be greater than zero");
    }
}
