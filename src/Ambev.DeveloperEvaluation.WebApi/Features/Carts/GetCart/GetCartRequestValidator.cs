using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Validator for GetCartRequest.
/// Ensures that the request contains valid data before processing.
/// </summary>
public class GetCartRequestValidator : AbstractValidator<GetCartRequest>
{
    /// <summary>
    /// Initializes validation rules for GetCartRequest.
    /// Ensures that the Id property is not empty or null.
    /// </summary>
    public GetCartRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}
