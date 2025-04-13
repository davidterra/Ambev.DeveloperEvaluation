using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
       RuleFor(x => x.UserId)
            .NotEmpty()
            .Must(x => x != Guid.Empty)
            .WithMessage("User ID cannot be empty.");
        RuleFor(x => x.BranchId)
            .GreaterThan(0)
            .WithMessage("Branch ID must be greater than zero.");
        RuleFor(x => x.Items)
            .NotNull()
            .WithMessage("Sale must contain at least one item.")
            .Must(items => items.All(item => item.Quantity > 0))
            .WithMessage("All items must have a quantity greater than zero.");
        RuleFor(x => x.TotalAmount)
            .NotNull()
            .WithMessage("Total amount must be greater than zero.");       
    }
}

