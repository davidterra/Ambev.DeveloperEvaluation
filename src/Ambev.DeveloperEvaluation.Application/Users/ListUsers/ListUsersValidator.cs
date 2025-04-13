using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Validator for the ListUsersCommand.
/// Ensures that the command's properties meet the required validation rules.
/// </summary>
public class ListUsersCommandValidator : AbstractValidator<ListUsersCommand>
{
    /// <summary>
    /// Initializes validation rules for the ListUsersCommand.
    /// </summary>
    public ListUsersCommandValidator()
    {
        // Validates the OrderBy property to ensure it matches the expected format.
        RuleFor(x => x.OrderBy)
            .Matches(@"""([a-zA-Z]+( (asc|desc))?(, )?)*[a-zA-Z]+( (asc|desc))?""")
            .When(x => !string.IsNullOrEmpty(x.OrderBy))
            .WithMessage("Order format is invalid.");
    }
}
