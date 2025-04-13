using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Result model for ListUsers operation.
/// Represents the details of a user retrieved in the ListUsers operation.
/// </summary>
public class ListUsersResult
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The username of the user.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's phone number.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// The user's role in the system.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// The current status of the user.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// The user's name details.
    /// </summary>
    public ListUsersNameResult Name { get; set; } = null!;

    /// <summary>
    /// The user's address details.
    /// </summary>
    public ListUsersAddressResult Address { get; set; } = null!;
}

/// <summary>
/// Represents the name details of a user.
/// </summary>
public class ListUsersNameResult
{
    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
}

/// <summary>
/// Represents the address details of a user.
/// </summary>
public class ListUsersAddressResult
{
    /// <summary>
    /// The street name of the user's address.
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// The city of the user's address.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// The state of the user's address.
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// The house or building number of the user's address.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// The zip code of the user's address.
    /// </summary>
    public string ZipCode { get; set; } = string.Empty;

    /// <summary>
    /// The geographical location details of the user's address.
    /// </summary>
    public ListUsersGeoLocationResult GeoLocation { get; set; } = null!;
}

/// <summary>
/// Represents the geographical location details of a user's address.
/// </summary>
public class ListUsersGeoLocationResult
{
    /// <summary>
    /// The latitude of the user's address.
    /// </summary>
    public string Latitude { get; set; } = string.Empty;

    /// <summary>
    /// The longitude of the user's address.
    /// </summary>
    public string Longitude { get; set; } = string.Empty;
}
