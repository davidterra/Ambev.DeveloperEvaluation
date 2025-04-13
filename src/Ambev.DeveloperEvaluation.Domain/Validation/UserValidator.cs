using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());

        RuleFor(user => user.Username)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");

        RuleFor(user => user.Password).SetValidator(new PasswordValidator());

        RuleFor(user => user.Phone)
            .Matches(@"^\+[1-9]\d{10,14}$")
            .WithMessage("Phone number must start with '+' followed by 11-15 digits.");

        RuleFor(user => user.Status)
            .NotEqual(UserStatus.Unknown)
            .WithMessage("User status cannot be Unknown.");

        RuleFor(user => user.Role)
            .NotEqual(UserRole.None)
            .WithMessage("User role cannot be None.");

        RuleFor(user => user.Name)
            .NotNull()
            .WithMessage("Name cannot be null.")
            .SetValidator(new NameValueValidator());

        RuleFor(user => user.Address)
            .NotNull()
            .WithMessage("Address cannot be null.")
            .SetValidator(new AddressValueValidator());
    }
}

public class NameValueValidator : AbstractValidator<PersonNameValue>
{
    public NameValueValidator()
    {
        RuleFor(name => name.FirstName)
            .NotEmpty()
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters.");
        RuleFor(name => name.LastName)
            .NotEmpty()
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
            .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters.");
    }
}

public class AddressValueValidator : AbstractValidator<AddressValue>
{
    public AddressValueValidator()
    {
        RuleFor(address => address.Street)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Street must be at least 5 characters long.")
            .MaximumLength(100).WithMessage("Street cannot be longer than 100 characters.");
        RuleFor(address => address.Number).NotEmpty().Length(1, 10);
        RuleFor(address => address.City)
            .NotEmpty()
            .MinimumLength(2).WithMessage("City must be at least 2 characters long.")
            .MaximumLength(100).WithMessage("City cannot be longer than 100 characters.");
        RuleFor(address => address.State)
            .NotEmpty()
            .Matches(@"^[A-Z]{2}$").WithMessage("State must be a valid 2-letter state code.");
        RuleFor(address => address.ZipCode)
            .NotEmpty()
            .Matches(@"^\d{5}-\d{3}$").WithMessage("Zip code must be in the format XXXXX-XXX.");
        RuleFor(address => address.GeoLocation)
            .SetValidator(new GeoLocationValueValidator());
    }
}

public class GeoLocationValueValidator : AbstractValidator<GeoLocationValue>
{
    public GeoLocationValueValidator()
    {

        RuleFor(geo => geo.Latitude)
            .NotEmpty()
            .Matches(@"^[-+]?\d+(\.\d+)?(,[-+]?\d+(\.\d+)?)?$")
            .WithMessage("Latitude must be a valid number between -90 and 90");


        RuleFor(geo => geo.Longitude)
            .NotEmpty()
            .Matches(@"^[-+]?\d+(\.\d+)?(,[-+]?\d+(\.\d+)?)?$")
            .WithMessage("Longitude must be a valid number between -180 and 180.");
    }
}