﻿using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Command for updating an existing user.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a user, 
/// including username, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="UpdateUserResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateUserCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class UpdateUserCommand : IRequest<UpdateUserResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the user to be updated.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user to be updated.
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
    public UpdateUserNameCommand Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the address details of the user.
    /// </summary>
    public UpdateUserAddressCommand Address { get; set; } = null!;

    /// <summary>
    /// Validates the current command instance using the <see cref="UpdateUserCommandValidator"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing the validation results, 
    /// including whether the command is valid and any validation errors.
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateUserCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Represents the name details of the user to be updated.
/// </summary>
public class UpdateUserNameCommand
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
/// Represents the address details of the user to be updated.
/// </summary>
public class UpdateUserAddressCommand
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
    public UpdateUserGeoLocationCommand GeoLocation { get; set; } = null!;
}

/// <summary>
/// Represents the geographical location details of the user's address.
/// </summary>
public class UpdateUserGeoLocationCommand
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
