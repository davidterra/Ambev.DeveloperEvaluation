using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Validator for the <see cref="GetUserCommand"/> class.
/// Ensures that the command contains valid data before processing.
/// </summary>
public class GetUserCommandValidator : AbstractValidator<GetUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserCommandValidator"/> class.
    /// Defines validation rules for the <see cref="GetUserCommand"/> properties.
    /// </summary>
    public GetUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}
