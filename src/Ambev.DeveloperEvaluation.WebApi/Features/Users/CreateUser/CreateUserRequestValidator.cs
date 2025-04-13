using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Validator for CreateUserRequest that defines validation rules for user creation.
/// </summary>
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
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
    /// - Name: Must be valid and follow CreateUserNameRequestValidator rules
    /// - Address: Must be valid and follow CreateUserAddressRequestValidator rules
    /// </remarks>
    public CreateUserRequestValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Username).NotEmpty().Length(3, 50);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
        RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
        RuleFor(user => user.Role).NotEqual(UserRole.None);
        RuleFor(user => user.Name).NotNull().SetValidator(new CreateUserNameRequestValidator());
        RuleFor(user => user.Address).NotNull().SetValidator(new CreateUserAddressRequestValidator());
    }
}

/// <summary>
/// Validator for CreateUserNameRequest that defines validation rules for user name.
/// </summary>
public class CreateUserNameRequestValidator : AbstractValidator<CreateUserNameRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserNameRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - FirstName: Required, length between 1 and 50 characters
    /// - LastName: Required, length between 1 and 50 characters
    /// </remarks>
    public CreateUserNameRequestValidator()
    {
        RuleFor(name => name.FirstName).NotEmpty().Length(1, 50);
        RuleFor(name => name.LastName).NotEmpty().Length(1, 50);
    }
}

/// <summary>
/// Validator for CreateUserAddressRequest that defines validation rules for user address.
/// </summary>
public class CreateUserAddressRequestValidator : AbstractValidator<CreateUserAddressRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserAddressRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Street: Required, length between 5 and 100 characters
    /// - Number: Required, length between 1 and 10 characters
    /// - City: Required, length between 2 and 100 characters
    /// - State: Required, must be a valid 2-letter state code
    /// - ZipCode: Required, must follow the format XXXXX-XXX
    /// - GeoLocation: Must be valid and follow CreateUserGeoLocationRequestValidator rules
    /// </remarks>
    public CreateUserAddressRequestValidator()
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
            .SetValidator(new CreateUserGeoLocationRequestValidator());
    }
}

/// <summary>
/// Validator for CreateUserGeoLocationRequest that defines validation rules for user geo-location.
/// </summary>
public class CreateUserGeoLocationRequestValidator : AbstractValidator<CreateUserGeoLocationRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserGeoLocationRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Latitude: Required, must be a valid number between -90 and 90
    /// - Longitude: Required, must be a valid number between -180 and 180
    /// </remarks>
    public CreateUserGeoLocationRequestValidator()
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
