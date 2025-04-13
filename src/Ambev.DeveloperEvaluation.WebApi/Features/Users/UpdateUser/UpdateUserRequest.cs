using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Represents a request to update an existing user in the system.
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the username. Must be unique and contain only valid characters.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password. Must meet security requirements.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number in format (XX) XXXXX-XXXX.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address. Must be a valid email format.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the initial status of the user account.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the role assigned to the user.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets the user's name details.
    /// </summary>
    public UpdateUserNameRequest Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user's address details.
    /// </summary>
    public UpdateUserAddressRequest Address { get; set; } = null!;
}

/// <summary>
/// Represents the user's name details for an update request.
/// </summary>
public class UpdateUserNameRequest
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
/// Represents the user's address details for an update request.
/// </summary>
public class UpdateUserAddressRequest
{
    /// <summary>
    /// Gets or sets the street name of the user's address.
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the house or building number of the user's address.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the city of the user's address.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the state of the user's address.
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the zip code of the user's address.
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geographical location of the user's address.
    /// </summary>
    public UpdateUserGeoLocationRequest GeoLocation { get; set; } = null!;
}

/// <summary>
/// Represents the geographical location details for an update request.
/// </summary>
public class UpdateUserGeoLocationRequest
{
    /// <summary>
    /// Gets or sets the latitude of the user's location.
    /// </summary>
    public string Latitude { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the longitude of the user's location.
    /// </summary>
    public string Longitude { get; set; } = string.Empty;
}
