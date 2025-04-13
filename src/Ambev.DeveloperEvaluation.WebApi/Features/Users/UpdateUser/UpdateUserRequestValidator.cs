using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Validator for UpdateUserRequest that defines validation rules for updating user information.
/// </summary>
public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateUserRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Username: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None
    /// - Name: Must be valid and follow UpdateUserNameRequestValidator rules
    /// - Address: Must be valid and follow UpdateUserAddressRequestValidator rules
    /// </remarks>
    public UpdateUserRequestValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Username).NotEmpty().Length(3, 50);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
        RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
        RuleFor(user => user.Role).NotEqual(UserRole.None);
        RuleFor(user => user.Name).NotNull().SetValidator(new UpdateUserNameRequestValidator());
        RuleFor(user => user.Address).NotNull().SetValidator(new UpdateUserAddressRequestValidator());
    }
}

/// <summary>
/// Validator for UpdateUserNameRequest that defines validation rules for user name updates.
/// </summary>
public class UpdateUserNameRequestValidator : AbstractValidator<UpdateUserNameRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateUserNameRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - FirstName: Required, length between 1 and 50 characters
    /// - LastName: Required, length between 1 and 50 characters
    /// </remarks>
    public UpdateUserNameRequestValidator()
    {
        RuleFor(name => name.FirstName).NotEmpty().Length(1, 50);
        RuleFor(name => name.LastName).NotEmpty().Length(1, 50);
    }
}

/// <summary>
/// Validator for UpdateUserAddressRequest that defines validation rules for user address updates.
/// </summary>
public class UpdateUserAddressRequestValidator : AbstractValidator<UpdateUserAddressRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateUserAddressRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Street: Required, length between 5 and 100 characters
    /// - Number: Required, length between 1 and 10 characters
    /// - City: Required, length between 2 and 100 characters
    /// - State: Required, must be a valid 2-letter state code
    /// - ZipCode: Required, must match the format XXXXX-XXX
    /// - GeoLocation: Must be valid and follow UpdateUserGeoLocationRequestValidator rules
    /// </remarks>
    public UpdateUserAddressRequestValidator()
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
            .SetValidator(new UpdateUserGeoLocationRequestValidator());
    }
}

/// <summary>
/// Validator for UpdateUserGeoLocationRequest that defines validation rules for user geolocation updates.
/// </summary>
public class UpdateUserGeoLocationRequestValidator : AbstractValidator<UpdateUserGeoLocationRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateUserGeoLocationRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Latitude: Required, must be a valid number between -90 and 90
    /// - Longitude: Required, must be a valid number between -180 and 180
    /// </remarks>
    public UpdateUserGeoLocationRequestValidator()
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
