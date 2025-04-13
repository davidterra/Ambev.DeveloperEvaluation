using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Command for creating a new user.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a user, 
/// including username, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateUserResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateUserCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateUserCommand : IRequest<CreateUserResult>
{
    /// <summary>
    /// Gets or sets the username of the user to be created.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number for the user.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address for the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of the user.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets the name details of the user.
    /// </summary>
    public CreateUserNameCommand Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the address details of the user.
    /// </summary>
    public CreateUserAddressCommand Address { get; set; } = null!;

    /// <summary>
    /// Validates the current command instance using the associated validator.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing the validation results, 
    /// including whether the command is valid and any validation errors.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateUserCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Represents the name details of the user.
/// </summary>
public class CreateUserNameCommand
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
}

/// <summary>
/// Represents the address details of the user.
/// </summary>
public class CreateUserAddressCommand
{
    /// <summary>
    /// Gets or sets the street address of the user.
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the city of the user.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the state of the user.
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the house or apartment number of the user.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the zip code of the user's address.
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geographical location of the user's address.
    /// </summary>
    public CreateUserGeoLocationCommand GeoLocation { get; set; } = null!;
}

/// <summary>
/// Represents the geographical location details of the user's address.
/// </summary>
public class CreateUserGeoLocationCommand
{
    /// <summary>
    /// Gets or sets the latitude of the user's address.
    /// </summary>
    public string Latitude { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the longitude of the user's address.
    /// </summary>
    public string Longitude { get; set; } = string.Empty;
}
