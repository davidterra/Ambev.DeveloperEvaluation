using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Validator for <see cref="GetUserRequest"/>.
/// Ensures that the request contains valid data for retrieving a user.
/// </summary>
public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserRequestValidator"/> class.
    /// Defines validation rules for the <see cref="GetUserRequest"/> model.
    /// </summary>
    public GetUserRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}
