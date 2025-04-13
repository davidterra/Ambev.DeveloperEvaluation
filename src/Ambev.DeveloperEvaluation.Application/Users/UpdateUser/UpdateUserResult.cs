using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Represents the response returned after successfully updating a user.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the updated user,
/// along with their updated details such as username, password, contact information,
/// status, role, name, and address.
/// </remarks>
public class UpdateUserResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
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
    public UpdateUserNameResult Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the address details of the user.
    /// </summary>
    public UpdateUserAddressResult Address { get; set; } = null!;
}

/// <summary>
/// Represents the name details of the user.
/// </summary>
public class UpdateUserNameResult
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
public class UpdateUserAddressResult
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
    /// Gets or sets the zip code of the user.
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geographical location of the user's address.
    /// </summary>
    public UpdateUserGeoLocationResult GeoLocation { get; set; } = null!;
}

/// <summary>
/// Represents the geographical location details of the user's address.
/// </summary>
public class UpdateUserGeoLocationResult
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
