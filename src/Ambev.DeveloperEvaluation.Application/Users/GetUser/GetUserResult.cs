using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Result model for GetUser operation
/// </summary>
public class GetUserResult
{
    /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The username of the user
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The user's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The user's role in the system
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// The current status of the user
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// The user's name details
    /// </summary>
    public GetUserNameResult Name { get; set; } = null!;

    /// <summary>
    /// The user's address details
    /// </summary>
    public GetUserAddressResult Address { get; set; } = null!;
}

/// <summary>
/// Represents the user's name details
/// </summary>
public class GetUserNameResult
{
    /// <summary>
    /// The user's first name
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// The user's last name
    /// </summary>
    public string LastName { get; set; } = string.Empty;
}

/// <summary>
/// Represents the user's address details
/// </summary>
public class GetUserAddressResult
{
    /// <summary>
    /// The street name of the user's address
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// The city of the user's address
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// The state of the user's address
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// The house or building number of the user's address
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// The zip code of the user's address
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// The geographical location of the user's address
    /// </summary>
    public GetUsersGeoLocationResult GeoLocation { get; set; } = null!;
}

/// <summary>
/// Represents the geographical location details
/// </summary>
public class GetUsersGeoLocationResult
{
    /// <summary>
    /// The latitude of the user's location
    /// </summary>
    public string Latitude { get; set; } = string.Empty;

    /// <summary>
    /// The longitude of the user's location
    /// </summary>
    public string Longitude { get; set; } = string.Empty;
}
