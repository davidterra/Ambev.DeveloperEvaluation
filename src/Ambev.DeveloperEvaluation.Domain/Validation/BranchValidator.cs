using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        RuleFor(branch => branch.Name)
            .NotEmpty().WithMessage("Branch name must not be empty.")
            .MinimumLength(3).WithMessage("Branch name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Branch name cannot be longer than 100 characters.");
    }
}
