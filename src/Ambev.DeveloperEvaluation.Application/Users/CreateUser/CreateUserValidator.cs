using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Validator for CreateUserCommand that defines validation rules for user creation command.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Username: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None
    /// </remarks>
    public CreateUserCommandValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Username).NotEmpty().Length(3, 50);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
        RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
        RuleFor(user => user.Role).NotEqual(UserRole.None);
        RuleFor(user => user.Name).NotNull().SetValidator(new CreateUserNameCommandValidator());
        RuleFor(user => user.Address).NotNull().SetValidator(new CreateUserAddressCommandValidator());
    }
}

public class CreateUserNameCommandValidator : AbstractValidator<CreateUserNameCommand>
{
    public CreateUserNameCommandValidator()
    {
        RuleFor(name => name.FirstName).NotEmpty().Length(1, 50);
        RuleFor(name => name.LastName).NotEmpty().Length(1, 50);
    }
}

public class CreateUserAddressCommandValidator : AbstractValidator<CreateUserAddressCommand>
{
    public CreateUserAddressCommandValidator()
    {
        RuleFor(address => address.Street).NotEmpty().Length(1, 100);
        RuleFor(address => address.Number).NotEmpty().Length(1, 10);
        RuleFor(address => address.City).NotEmpty().Length(1, 100);
        RuleFor(address => address.State).NotEmpty().Length(2, 2)
            .Matches(@"^[A-Z]{2}$").WithMessage("State must be a valid 2-letter state code.");
        RuleFor(address => address.ZipCode)
            .NotEmpty()
            .Matches(@"^\d{5}-\d{3}$")
            .WithMessage("ZipCode must be in the format XXXXX-XXX.");
        RuleFor(address => address.GeoLocation).NotNull().SetValidator(new CreateUserGeoLocationCommandValidator());
    }
}

public class CreateUserGeoLocationCommandValidator : AbstractValidator<CreateUserGeoLocationCommand>
{
    public CreateUserGeoLocationCommandValidator()
    {
        RuleFor(geo => geo.Latitude)
            .NotEmpty()
            .WithMessage("Latitude must be a valid number between -90 and 90.");

        RuleFor(geo => geo.Longitude)
            .NotEmpty()
            .WithMessage("Longitude must be a valid number between -180 and 180.");
    }
}